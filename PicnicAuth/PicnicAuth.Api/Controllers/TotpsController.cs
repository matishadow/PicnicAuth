using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
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
        /// Get Totp for a user
        /// </summary>
        /// <returns>Totp</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
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
        /// Get Totp for a user
        /// </summary>
        /// <returns>Totp</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
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