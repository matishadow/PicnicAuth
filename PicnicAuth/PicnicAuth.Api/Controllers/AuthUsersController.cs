using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.SwaggerResponses;
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
    /// Controller to manage AuthUsers by logged company.
    /// </summary>
    public class AuthUsersController : BasePicnicAuthController
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
        public AuthUsersController(IMapper autoMapper, IUnitOfWork unitOfWork, ISecretGenerator secretGenerator,
            IDpapiEncryptor dpapiEncryptor, IBase32Encoder base32Encoder,
            IOtpQrCodeUriGenerator otpQrCodeUriGenerator, IAuthUserDtoFiller authUserDtoFiller) : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.secretGenerator = secretGenerator;
            this.dpapiEncryptor = dpapiEncryptor;
            this.base32Encoder = base32Encoder;
            this.otpQrCodeUriGenerator = otpQrCodeUriGenerator;
            this.authUserDtoFiller = authUserDtoFiller;
        }

        /// <summary>
        /// Create new AuthUser and add it to logged company's collection.
        /// </summary>
        /// <param name="addAuthUser">AuthUser creation model.</param>
        /// <returns>Created AuthUser</returns>
        [SwaggerCompanyNotLoggedInResponse]
        [SwaggerResponse(HttpStatusCode.Created, "AuthUsers has been created.", Type = typeof(AuthUserDto))]
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
            authUserDtoFiller.FillAuthUserDto(
                authUserDto, Request, authUser, loggedCompany, secret, otpQrCodeUriGenerator, base32Encoder);

            return Created(string.Empty, authUserDto);
        }

    }
}
