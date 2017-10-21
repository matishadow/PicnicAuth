using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Web;
using PicnicAuth.Dto;
using PicnicAuth.Models;
using PicnicAuth.Models.Authentication;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class AuthUsersController : BasePicnicAuthController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISecretGenerator secretGenerator;
        private readonly IDpapiEncryptor dpapiEncryptor;
        private readonly IBase32Encoder base32Encoder;
        private readonly IOtpQrCodeUriGenerator otpQrCodeUriGenerator;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="autoMapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="secretGenerator"></param>
        /// <param name="dpapiEncryptor"></param>
        /// <param name="base32Encoder"></param>
        /// <param name="otpQrCodeUriGenerator"></param>
        public AuthUsersController(IMapper autoMapper, IUnitOfWork unitOfWork, ISecretGenerator secretGenerator,
            IDpapiEncryptor dpapiEncryptor, IBase32Encoder base32Encoder,
            IOtpQrCodeUriGenerator otpQrCodeUriGenerator) : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.secretGenerator = secretGenerator;
            this.dpapiEncryptor = dpapiEncryptor;
            this.base32Encoder = base32Encoder;
            this.otpQrCodeUriGenerator = otpQrCodeUriGenerator;
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
            byte[] secret = secretGenerator.GenerateSecret();
            authUser.Secret = dpapiEncryptor.Encrypt(secret);
            loggedCompany.AuthUsers.Add(authUser);
            repository.Save();

            AuthUserDto authUserDto = AutoMapper.Map<AuthUser, AuthUserDto>(authUser);
            authUserDto.TotpQrCodeUri = otpQrCodeUriGenerator
                .GenerateQrCodeUri(OtpType.Totp, Request, authUser.Id, loggedCompany.UserName);
            authUserDto.HotpQrCodeUri = otpQrCodeUriGenerator
                .GenerateQrCodeUri(OtpType.Hotp, Request, authUser.Id, loggedCompany.UserName);
            authUserDto.SecretInBase32 = base32Encoder.Encode(secret);

            return Created(string.Empty, authUserDto);
        }

    }
}
