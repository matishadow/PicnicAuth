using System.Web.Http;
using AutoMapper;
using FriGo.Api.Filters;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    [SwaggerResponseRemoveDefaults]
    [FrigoAuthorize]
    public abstract class BaseFriGoController : ApiController
    {
        protected readonly IMapper AutoMapper;
        protected readonly IValidatingService ValidatingService;
        protected readonly IOwningService OwningService;

        protected BaseFriGoController(IMapper autoMapper, IValidatingService validatingService,
            IOwningService owningService)
        {
            AutoMapper = autoMapper;
            ValidatingService = validatingService;
            OwningService = owningService;
        }
    }
}
