using System.Web.Http;
using AutoMapper;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Inherit from this class if you want to have Automapper instance
    /// in your controller. Pretty common scenario.
    /// </summary>
    [SwaggerResponseRemoveDefaults]
    public abstract class BasePicnicAuthController : ApiController
    {
        /// <summary>
        /// Use this instead of static automapper 
        /// to be able to easily mock mapping.
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
