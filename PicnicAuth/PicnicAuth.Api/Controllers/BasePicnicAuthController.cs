using System.Web.Http;
using AutoMapper;
using PicnicAuth.Api.Filters;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    [SwaggerResponseRemoveDefaults]
    public abstract class BasePicnicAuthController : ApiController
    {
        protected readonly IMapper AutoMapper;

        protected BasePicnicAuthController(IMapper autoMapper)
        {
            AutoMapper = autoMapper;
        }
    }
}
