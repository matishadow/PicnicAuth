using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
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
        /// Get Totp for a user
        /// </summary>
        /// <returns>Totp</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
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
        /// Get Totp for a user
        /// </summary>
        /// <returns>Totp</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
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
