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
    public class TotpsController : BasePicnicAuthController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDpapiDecryptor dpapiDecryptor;
        private readonly ITotpValidator totpValidator;
        private readonly ITotpGenerator totpGenerator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autoMapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="dpapiDecryptor"></param>
        /// <param name="totpValidator"></param>
        /// <param name="totpGenerator"></param>
        public TotpsController(IMapper autoMapper, IUnitOfWork unitOfWork, IDpapiDecryptor dpapiDecryptor,
            ITotpValidator totpValidator, ITotpGenerator totpGenerator) : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.dpapiDecryptor = dpapiDecryptor;
            this.totpValidator = totpValidator;
            this.totpGenerator = totpGenerator;
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
            IGenericRepository<CompanyAccount> repository = unitOfWork.Repository<CompanyAccount>();
            string loggedCompanyId = RequestContext.Principal.Identity.GetUserId();
            CompanyAccount loggedCompany = repository.GetById(loggedCompanyId);

            AuthUser authUser = loggedCompany.AuthUsers.SingleOrDefault(user => user.Id == userId);
            if (authUser == null) return NotFound();
            byte[] decryptedSecret = dpapiDecryptor.DecryptToBytes(authUser.Secret);

            var totpPassword = new OneTimePassword { OtpValue = totpGenerator.GenerateTotp(decryptedSecret) };

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
            IGenericRepository<CompanyAccount> repository = unitOfWork.Repository<CompanyAccount>();
            string loggedCompanyId = RequestContext.Principal.Identity.GetUserId();
            CompanyAccount loggedCompany = repository.GetById(loggedCompanyId);

            AuthUser authUser = loggedCompany.AuthUsers.SingleOrDefault(user => user.Id == userId);
            if (authUser == null) return NotFound();
            byte[] decryptedSecret = dpapiDecryptor.DecryptToBytes(authUser.Secret);

            bool isOtpValid = totpValidator.IsTotpValid(decryptedSecret, totp);
            var validationResult = new OtpValidationResult { IsOtpValid = isOtpValid };

            return Ok(validationResult);
        }
    }
}
