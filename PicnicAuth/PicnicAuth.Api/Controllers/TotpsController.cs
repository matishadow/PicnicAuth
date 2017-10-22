using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using PicnicAuth.Database.SwaggerResponses;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Web;
using PicnicAuth.Models;
using PicnicAuth.Models.Authentication;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller used to manage Otps based on current time.
    /// </summary>
    public class TotpsController : BasePicnicAuthController
    {
        private readonly ILoggedCompanyGetter loggedCompanyGetter;
        private readonly IDpapiDecryptor dpapiDecryptor;

        private readonly ITotpValidator totpValidator;
        private readonly ITotpGenerator totpGenerator;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="autoMapper"></param>
        /// <param name="dpapiDecryptor"></param>
        /// <param name="totpValidator"></param>
        /// <param name="totpGenerator"></param>
        /// <param name="loggedCompanyGetter"></param>
        public TotpsController(IMapper autoMapper, IDpapiDecryptor dpapiDecryptor,
            ITotpValidator totpValidator, ITotpGenerator totpGenerator,
            ILoggedCompanyGetter loggedCompanyGetter) : base(autoMapper)
        {
            this.dpapiDecryptor = dpapiDecryptor;
            this.totpValidator = totpValidator;
            this.totpGenerator = totpGenerator;
            this.loggedCompanyGetter = loggedCompanyGetter;
        }

        /// <summary>
        /// Get Totp for given AuthUser. 
        /// </summary>
        /// <param name="userId">AuthUser's id in Guid format.</param>
        /// <returns>Time-based One-time Password</returns>
        [SwaggerResponse(HttpStatusCode.OK, "AuthUser's Totp", typeof(OneTimePassword))]
        [SwaggerAuthUserNotFoundResponse]
        [SwaggerCompanyNotLoggedInResponse]
        [Route("api/AuthUsers/{userId}/totp")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetTotpForUser(Guid userId)
        {
            CompanyAccount loggedCompany = loggedCompanyGetter.GetLoggedCompany(RequestContext);

            AuthUser authUser = loggedCompany.AuthUsers.SingleOrDefault(user => user.Id == userId);
            if (authUser == null) return NotFound();
            byte[] decryptedSecret = dpapiDecryptor.DecryptToBytes(authUser.Secret);

            var totpPassword = new OneTimePassword {OtpValue = totpGenerator.GenerateTotp(decryptedSecret)};

            return Ok(totpPassword);
        }


        /// <summary>
        /// Validate given Totp for AuthUser
        /// </summary>
        /// <param name="userId">AuthUser's id in Guid format.</param>
        /// <param name="totp">OTP to check.</param>
        /// <returns>Validation result</returns>
        [SwaggerOtpValidationDoneResponse]
        [SwaggerAuthUserNotFoundResponse]
        [SwaggerCompanyNotLoggedInResponse]
        [Route("api/AuthUsers/{userId}/totp/{totp}")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult ValidateTotp(Guid userId, string totp)
        {
            CompanyAccount loggedCompany = loggedCompanyGetter.GetLoggedCompany(RequestContext);

            AuthUser authUser = loggedCompany.AuthUsers.SingleOrDefault(user => user.Id == userId);
            if (authUser == null) return NotFound();
            byte[] decryptedSecret = dpapiDecryptor.DecryptToBytes(authUser.Secret);

            bool isOtpValid = totpValidator.IsTotpValid(decryptedSecret, totp);
            var validationResult = new OtpValidationResult {IsOtpValid = isOtpValid};

            return Ok(validationResult);
        }
    }
}