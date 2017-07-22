using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using FluentValidation.Results;
using FriGo.Api.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using FriGo.Api.Providers;
using FriGo.Db.DTO;
using FriGo.Db.DTO.Account;
using FriGo.Db.DTO.Ingredients;
using FriGo.Db.Models;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    [FrigoAuthorize]
    public class AccountController : ApiController 
    {
        private ApplicationUserManager userManager;
        private readonly IValidatingService validatingService;

        private readonly IChangePasswordValidator changePasswordValidator;
        private readonly IRegisterValidator registerValidator;

        public AccountController(IValidatingService validatingService, IChangePasswordValidator changePasswordValidator,
            IRegisterValidator registerValidator)
        {
            this.validatingService = validatingService;
            this.changePasswordValidator = changePasswordValidator;
            this.registerValidator = registerValidator;
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { userManager = value; }
        }

        /// <summary>
        /// Gets information about current user
        /// </summary>
        /// <returns>Information about logged user</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserInfoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Username = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin?.LoginProvider
            };
        }

        /// <summary>
        /// Method used in order to change password of current user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, Description = "Password changed")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/Account/ChangePassword")]
        public HttpResponseMessage ChangePassword(ChangePasswordBindingModel model)
        {
            if (!validatingService.IsValid(changePasswordValidator, model))
                return Request.CreateResponse(validatingService.GetStatusCode(),
                    validatingService.GenerateError(changePasswordValidator, model));

            IdentityResult result = UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword).Result;

            return !result.Succeeded ? GetErrorResult(result) : Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Account created</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(RegisterBindingModel model)
        {
            if (!validatingService.IsValid(registerValidator, model))
                return Request.CreateResponse(validatingService.GetStatusCode(),
                    validatingService.GenerateError(registerValidator, model));

            var user = new User {UserName = model.Username, Email = model.Email};

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            return !result.Succeeded
                ? Request.CreateErrorResponse(HttpStatusCode.InternalServerError, GetErrorResult(result).ToString())
                : Request.CreateResponse(HttpStatusCode.Created);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
                userManager = null;
            }

            base.Dispose(disposing);
        }

        private HttpResponseMessage GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            if (result.Succeeded) return null;

            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure(nameof(ChangePasswordBindingModel.OldPassword),
                    Db.Properties.Resources.CurrentPasswordValidationError)
            });

            return Request.CreateResponse(HttpStatusCode.BadRequest, validationResult);
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                Claim providerKeyClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(providerKeyClaim?.Issuer) || string.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }
    }
}
