﻿using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.OneTimePassword;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthUsersController : BasePicnicAuthController
    {
        private readonly string QrCodeUriTemplate = "{0}/api/qrcodes/{1}?issuer={2}";

        private readonly IUnitOfWork unitOfWork;
        private readonly ISecretGenerator secretGenerator;
        private readonly IDpapiEncryptor dpapiEncryptor;
        private readonly ITotpGenerator totpGenerator;
        private readonly IDpapiDecryptor dpapiDecryptor;
        private readonly ITotpValidator totpValidator;

        public AuthUsersController(IMapper autoMapper, IUnitOfWork unitOfWork,
            ISecretGenerator secretGenerator,
            IDpapiEncryptor dpapiEncryptor, 
            ITotpGenerator totpGenerator, 
            IDpapiDecryptor dpapiDecryptor, ITotpValidator totpValidator) : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.secretGenerator = secretGenerator;
            this.dpapiEncryptor = dpapiEncryptor;
            this.totpGenerator = totpGenerator;
            this.dpapiDecryptor = dpapiDecryptor;
            this.totpValidator = totpValidator;
        }

        /// <summary>
        /// Create a new company account.
        /// </summary>
        /// <returns>Account created</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
        [Route("api/AuthUsers")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddUser(AddAuthUser addAuthUser)
        {
            IGenericRepository<CompanyAccount> repository = unitOfWork.Repository<CompanyAccount>();
            string loggedCompanyId = RequestContext.Principal.Identity.GetUserId();
            CompanyAccount loggedCompany = repository.GetById(loggedCompanyId);

            AuthUser authUser = AutoMapper.Map<AddAuthUser, AuthUser>(addAuthUser);
            authUser.Secret = dpapiEncryptor.Encrypt(secretGenerator.GenerateSecret());
            loggedCompany.AuthUsers.Add(authUser);
            repository.Save();

            AuthUserDto authUserDto = AutoMapper.Map<AuthUser, AuthUserDto>(authUser);
            authUserDto.QrCodeUri =
                new Uri(string.Format(QrCodeUriTemplate,
                    Request.RequestUri.GetLeftPart(UriPartial.Authority),
                    authUser.Id, loggedCompany.UserName));

            return Created(string.Empty, authUserDto);
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

            var totpPassword = new TotpPassword {TotpValue = totpGenerator.GenerateTotp(decryptedSecret)};

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
            var validationResult = new OtpValidationResult {IsOtpValid = isOtpValid};

            return Ok(validationResult);
        }
    }
}
