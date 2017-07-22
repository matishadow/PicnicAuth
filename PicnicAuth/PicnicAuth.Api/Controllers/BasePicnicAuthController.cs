using System.Web.Http;
using AutoMapper;
using PicnicAuth.Api.Filters;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    [SwaggerResponseRemoveDefaults]
    [PicnicAuthAuthorize]
    public abstract class BasePicnicAuthController : ApiController
    {
        protected readonly IMapper AutoMapper;
        protected readonly IValidatingService ValidatingService;
        protected readonly IOwningService OwningService;

        protected BasePicnicAuthController(IMapper autoMapper, IValidatingService validatingService,
            IOwningService owningService)
        {
            AutoMapper = autoMapper;
            ValidatingService = validatingService;
            OwningService = owningService;
        }
    }
}
