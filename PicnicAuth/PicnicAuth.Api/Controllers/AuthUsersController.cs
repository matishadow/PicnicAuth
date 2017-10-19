using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.ModelValidators.Interfaces;
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

        public AuthUsersController(IMapper autoMapper, IUnitOfWork unitOfWork, 
            ISecretGenerator secretGenerator,
            IDpapiEncryptor dpapiEncryptor) : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.secretGenerator = secretGenerator;
            this.dpapiEncryptor = dpapiEncryptor;
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
    }
}
