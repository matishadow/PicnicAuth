using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PicnicAuth.Database.DTO.Social;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    public class TagController : BasePicnicAuthController
    {
        private readonly ITagService tagService;

        public TagController(IMapper autoMapper, IValidatingService validatingService, IOwningService owningService,
            ITagService tagService) : base(autoMapper, validatingService, owningService)
        {
            this.tagService = tagService;
        }

        /// <summary>
        /// Returns all tags
        /// </summary>
        /// <returns>An array of tags</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<TagDto>))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get()
        {
            IEnumerable<Tag> tags = tagService.Get().ToList();

            IEnumerable<TagDto> tagDtos = AutoMapper.Map<IEnumerable<Tag>, IEnumerable<TagDto>>(tags);

            return Request.CreateResponse(HttpStatusCode.OK, tagDtos);
        }       
    }
}
