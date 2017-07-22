using System.Net;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DTO.Social;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    public class UserController : BasePicnicAuthController
    {
        private readonly IUserService userService;

        public UserController(IMapper autoMapper, IValidatingService validatingService,
            IOwningService owningService, IUserService userService) : base(autoMapper, validatingService, owningService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get user info
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserDto))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        public virtual HttpResponseMessage Get()
        {
            User user = userService.Get(User.Identity.GetUserId());
            UserDto userDto = AutoMapper.Map<User, UserDto>(user);

            return Request.CreateResponse(HttpStatusCode.OK, userDto);
        }      
    }
}
