using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
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
    /// <inheritdoc />
    /// <summary>
    /// aawda
    /// </summary>
    public class TotpController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITotpGenerator totpGenerator;
        private readonly IDpapiDecryptor dpapiDecryptor;

        public TotpController(IUnitOfWork unitOfWork, ITotpGenerator totpGenerator,
            IDpapiDecryptor dpapiDecryptor)
        {
            this.unitOfWork = unitOfWork;
            this.totpGenerator = totpGenerator;
            this.dpapiDecryptor = dpapiDecryptor;
        }

        /// <summary>
        /// Get Totp for a user
        /// </summary>
        /// <returns>Totp</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
        [Route("api/totp/{userId}")]
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
    }
}
