using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PicnicAuth.Api.Filters;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    public class AccountController : ApiController 
    {
        private ApplicationUserManager userManager;

        private readonly IChangePasswordValidator changePasswordValidator;
        private readonly IRegisterValidator registerValidator;

        public AccountController(IChangePasswordValidator changePasswordValidator,
            IRegisterValidator registerValidator)
        {
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
        [SwaggerResponse(HttpStatusCode.Unauthorized, Description = "Not logged in")]
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
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Description = "Not logged in")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/Account/ChangePassword")]
        public HttpResponseMessage ChangePassword(ChangePasswordBindingModel model)
        {
            IdentityResult result = UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword).Result;

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Account created</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(RegisterBindingModel model)
        {
            var user = new User {UserName = model.Username, Email = model.Email};

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            return !result.Succeeded
                ? Request.CreateResponse(HttpStatusCode.InternalServerError, result)
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
