using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using PicnicAuth.Database.DAL;
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
    /// Controller used to manage Otps based on AuthUser's counter.
    /// </summary>
    public class HotpsController : BasePicnicAuthController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggedCompanyGetter loggedCompanyGetter;
        private readonly IDpapiDecryptor dpapiDecryptor;

        private readonly IHotpValidator hotpValidator;
        private readonly IHotpGenerator hotpGenerator;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="autoMapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="hotpGenerator"></param>
        /// <param name="dpapiDecryptor"></param>
        /// <param name="hotpValidator"></param>
        /// <param name="loggedCompanyGetter"></param>
        public HotpsController(IMapper autoMapper, IUnitOfWork unitOfWork, IHotpGenerator hotpGenerator,
            IDpapiDecryptor dpapiDecryptor, IHotpValidator hotpValidator,
            ILoggedCompanyGetter loggedCompanyGetter) : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.hotpGenerator = hotpGenerator;
            this.dpapiDecryptor = dpapiDecryptor;
            this.hotpValidator = hotpValidator;
            this.loggedCompanyGetter = loggedCompanyGetter;
        }


        /// <summary>
        /// Get Hotp for given AuthUser. 
        /// </summary>
        /// <param name="userId">AuthUser's id in Guid format.</param>
        /// <returns>HMAC-based One-time Password</returns>
        [SwaggerResponse(HttpStatusCode.OK, "AuthUser's Hotp", typeof(OneTimePassword))]
        [SwaggerAuthUserNotFoundResponse]
        [SwaggerCompanyNotLoggedInResponse]
        [Route("api/AuthUsers/{userId}/hotp")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetHotpForUser(Guid userId)
        {
            IGenericRepository<CompanyAccount> companyRepository = unitOfWork.Repository<CompanyAccount>();
            CompanyAccount loggedCompany = loggedCompanyGetter.GetLoggedCompany(RequestContext);

            AuthUser authUser = loggedCompany.AuthUsers.SingleOrDefault(user => user.Id == userId);
            if (authUser == null) return NotFound();
            byte[] decryptedSecret = dpapiDecryptor.DecryptToBytes(authUser.Secret);

            var hotpPassword = new OneTimePassword
            {
                OtpValue = hotpGenerator.GenerateHotp(authUser.HotpCounter, decryptedSecret)
            };
            authUser.HotpCounter++;
            companyRepository.Edit(loggedCompany);
            companyRepository.Save();

            return Ok(hotpPassword);
        }


        /// <summary>
        /// Validate given Hotp for AuthUser
        /// </summary>
        /// <param name="userId">AuthUser's id in Guid format.</param>
        /// <param name="hotp">OTP to check.</param>
        /// <returns>Validation result</returns>
        [SwaggerOtpValidationDoneResponse]
        [SwaggerAuthUserNotFoundResponse]
        [SwaggerCompanyNotLoggedInResponse]
        [Route("api/AuthUsers/{userId}/hotp/{hotp}")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult ValidateHotp(Guid userId, string hotp)
        {
            IGenericRepository<CompanyAccount> companyRepository = unitOfWork.Repository<CompanyAccount>();
            CompanyAccount loggedCompany = loggedCompanyGetter.GetLoggedCompany(RequestContext);

            AuthUser authUser = loggedCompany.AuthUsers.SingleOrDefault(user => user.Id == userId);
            if (authUser == null) return NotFound();
            byte[] decryptedSecret = dpapiDecryptor.DecryptToBytes(authUser.Secret);

            bool isOtpValid = hotpValidator.IsHotpValidInWindow(authUser.HotpCounter, decryptedSecret, hotp,
                counter => authUser.HotpCounter = counter);
            companyRepository.Edit(loggedCompany);
            companyRepository.Save();
            var validationResult = new OtpValidationResult { IsOtpValid = isOtpValid };

            return Ok(validationResult);
        }
    }
}
