using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
using PicnicAuth.Dto;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.Encoding;
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
    public class AuthUsersSecretsController : BasePicnicAuthController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISecretGenerator secretGenerator;
        private readonly IDpapiEncryptor dpapiEncryptor;
        private readonly IBase32Encoder base32Encoder;
        private readonly IOtpQrCodeUriGenerator otpQrCodeUriGenerator;
        private readonly IAuthUserDtoFiller authUserDtoFiller;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="autoMapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="secretGenerator"></param>
        /// <param name="dpapiEncryptor"></param>
        /// <param name="base32Encoder"></param>
        /// <param name="otpQrCodeUriGenerator"></param>
        /// <param name="authUserDtoFiller"></param>
        public AuthUsersSecretsController(IMapper autoMapper, IUnitOfWork unitOfWork,
            ISecretGenerator secretGenerator, IDpapiEncryptor dpapiEncryptor,
            IBase32Encoder base32Encoder, IOtpQrCodeUriGenerator otpQrCodeUriGenerator,
            IAuthUserDtoFiller authUserDtoFiller) : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.secretGenerator = secretGenerator;
            this.dpapiEncryptor = dpapiEncryptor;
            this.base32Encoder = base32Encoder;
            this.otpQrCodeUriGenerator = otpQrCodeUriGenerator;
            this.authUserDtoFiller = authUserDtoFiller;
        }

        /// <summary>
        /// Get Totp for a user
        /// </summary>
        /// <returns>Totp</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
        [Route("api/AuthUsers/{userId}/secret")]
        [HttpPatch]
        [Authorize]
        public IHttpActionResult GenerateNewSecret(Guid userId)
        {
            IGenericRepository<CompanyAccount> companyRepository = unitOfWork.Repository<CompanyAccount>();

            string loggedCompanyId = RequestContext.Principal.Identity.GetUserId();
            CompanyAccount loggedCompany = companyRepository.GetById(loggedCompanyId);

            AuthUser authUser = loggedCompany.AuthUsers.SingleOrDefault(user => user.Id == userId);
            if (authUser == null) return NotFound();

            byte[] secret = secretGenerator.GenerateSecret();
            authUser.Secret = dpapiEncryptor.Encrypt(secret);
            companyRepository.Save();

            AuthUserDto authUserDto = AutoMapper.Map<AuthUser, AuthUserDto>(authUser);
            authUserDtoFiller.FillAuthUserDto(
                authUserDto, Request, authUser, loggedCompany, secret, otpQrCodeUriGenerator, base32Encoder);

            return Ok(authUserDto);
        }
    }
}
