using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PicnicAuth.Api.Configs;
using PicnicAuth.Database.DAL;
using Swashbuckle.Swagger.Annotations;
using PicnicAuth.Dto;
using PicnicAuth.Interfaces.Validation;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class CompaniesController : BasePicnicAuthController
    {
        private CompanyManager companyManager;
        private CompanyManager CompanyManager =>
            companyManager ?? Request.GetOwinContext().GetUserManager<CompanyManager>();

        private readonly IUnitOfWork unitOfWork;
        private readonly IChangePasswordValidator changePasswordValidator;
        private readonly IRegisterValidator registerValidator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autoMapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="changePasswordValidator"></param>
        /// <param name="registerValidator"></param>
        public CompaniesController(IMapper autoMapper, IUnitOfWork unitOfWork,
            IChangePasswordValidator changePasswordValidator, IRegisterValidator registerValidator)
            : base(autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.changePasswordValidator = changePasswordValidator;
            this.registerValidator = registerValidator;
        }

        /// <summary>
        /// Get information about logged company.
        /// </summary>
        /// <returns>Information about logged company</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CompanyInfoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Description = "Not logged in")]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/Companies/Me")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetLoggedCompany()
        {
            IGenericRepository<CompanyAccount> repository = unitOfWork.Repository<CompanyAccount>();
            string loggedCompanyId = RequestContext.Principal.Identity.GetUserId();
            CompanyAccount loggedCompany = repository.GetById(loggedCompanyId);

            CompanyAccountDto companyAccountDto =
                AutoMapper.Map<CompanyAccount, CompanyAccountDto>(loggedCompany);

            return Ok(companyAccountDto);
        }

        /// <summary>
        /// Create a new company account.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Account created</returns>
        [SwaggerResponse(HttpStatusCode.Created, Description = "Company account created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Provided data was not valid")]
        [Route("api/Companies")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(RegisterBindingModel model)
        {
            CompanyAccount companyAccount = AutoMapper.Map<RegisterBindingModel, CompanyAccount>(model);

            IdentityResult result = await CompanyManager.CreateAsync(companyAccount, model.Password);

            return !result.Succeeded
                ? Request.CreateResponse(HttpStatusCode.InternalServerError, result)
                : Request.CreateResponse(HttpStatusCode.Created);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && companyManager != null)
            {
                companyManager.Dispose();
                companyManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
