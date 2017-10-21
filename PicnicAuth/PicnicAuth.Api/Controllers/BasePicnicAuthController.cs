using System.Web.Http;
using AutoMapper;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [SwaggerResponseRemoveDefaults]
    public abstract class BasePicnicAuthController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IMapper AutoMapper;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="autoMapper"></param>
        protected BasePicnicAuthController(IMapper autoMapper)
        {
            AutoMapper = autoMapper;
        }
    }
}
