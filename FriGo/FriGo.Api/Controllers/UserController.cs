using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Recipes;
using FriGo.ServiceInterfaces;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    public class UserController : BaseFriGoController
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
