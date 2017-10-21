using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Web;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HotpsController : BasePicnicAuthController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHotpGenerator hotpGenerator;
        private readonly IDpapiDecryptor dpapiDecryptor;
        private readonly IHotpValidator hotpValidator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autoMapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="hotpGenerator"></param>
        /// <param name="dpapiDecryptor"></param>
        /// <param name="hotpValidator"></param>
        public HotpsController(IMapper autoMapper, IUnitOfWork unitOfWork, IHotpGenerator hotpGenerator,
            IDpapiDecryptor dpapiDecryptor, IHotpValidator hotpValidator) : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.hotpGenerator = hotpGenerator;
            this.dpapiDecryptor = dpapiDecryptor;
            this.hotpValidator = hotpValidator;
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

            string loggedCompanyId = RequestContext.Principal.Identity.GetUserId();
            CompanyAccount loggedCompany = companyRepository.GetById(loggedCompanyId);

            AuthUser authUser = loggedCompany.AuthUsers.SingleOrDefault(user => user.Id == userId);
            if (authUser == null) return NotFound();
            byte[] decryptedSecret = dpapiDecryptor.DecryptToBytes(authUser.Secret);

            var hotpPassword = new OneTimePassword { OtpValue = hotpGenerator.GenerateHotp(authUser.HotpCounter, decryptedSecret) };
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
            string loggedCompanyId = RequestContext.Principal.Identity.GetUserId();
            CompanyAccount loggedCompany = companyRepository.GetById(loggedCompanyId);

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
