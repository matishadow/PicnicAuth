using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.ModelValidators.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CompaniesController : ApiController 
    {
        private CompanyManager companyManager;

        private readonly IChangePasswordValidator changePasswordValidator;
        private readonly IRegisterValidator registerValidator;

        public CompaniesController(IChangePasswordValidator changePasswordValidator,
            IRegisterValidator registerValidator)
        {
            this.changePasswordValidator = changePasswordValidator;
            this.registerValidator = registerValidator;
        }

        public CompanyManager CompanyManager
        {
            get => companyManager ?? Request.GetOwinContext().GetUserManager<CompanyManager>();
            private set => companyManager = value;
        }

        /// <summary>
        /// Get information about logged company.
        /// </summary>
        /// <returns>Information about logged company</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CompanyInfoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Description = "Not logged in")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/Companies/Me")]
        [HttpGet]
        [Authorize]
        public CompanyInfoViewModel Get()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new CompanyInfoViewModel
            {
                Login = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin?.LoginProvider
            };
        }

        /// <summary>
        /// Create a new company account.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Account created</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
        [Route("api/Companies")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(RegisterBindingModel model)
        {
            var companyAccount = new CompanyAccount {UserName = model.Login, Email = model.Email};

            IdentityResult result = await CompanyManager.CreateAsync(companyAccount, model.Password);

            return !result.Succeeded
                ? Request.CreateResponse(HttpStatusCode.InternalServerError, result)
                : Request.CreateResponse(HttpStatusCode.Created);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && companyManager != null)
            {
                companyManager.Dispose();
                companyManager = null;
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
