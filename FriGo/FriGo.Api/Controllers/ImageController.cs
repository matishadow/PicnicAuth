using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models;
using FriGo.Db.Models.Social;
using Swashbuckle.Swagger.Annotations;
using FriGo.Db.Models.Recipes;
using FriGo.ServiceInterfaces;
using Microsoft.AspNet.Identity;

namespace FriGo.Api.Controllers
{
    public class ImageController : BaseFriGoController
    {
        private const int FirstFileIndex = 0;

        private readonly IImageService imageService;

        public ImageController(IMapper autoMapper, IValidatingService validatingService, IOwningService owningService,
            IImageService imageService) : base(autoMapper, validatingService, owningService)
        {
            this.imageService = imageService;
        }

        /// <summary>
        /// Get image
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Binary stream of image</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(HttpResponseMessage))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get(Guid id)
        {
            var returnMessage = new HttpResponseMessage();

            Image image = imageService.Get(id);

            returnMessage.Content = new ByteArrayContent(image != null ? image.ImageBytes : new byte[]{});
            returnMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(Properties.Resources.PngMediaHeader);

            return returnMessage;
        }


        /// <summary>
        /// Upload image 
        /// </summary>
        /// <returns>Uri to uploaded image</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(Uri), Description = "Image uploaded")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.NotAcceptable, Type = typeof(MessageError), Description = "Not image mime multipart")]
        public virtual HttpResponseMessage Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
                return Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new MessageError(HttpStatusCode.NotAcceptable, Properties.Resources.IsNotMultipartMessage));

            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpPostedFile file = httpRequest.Files[FirstFileIndex];

            byte[] contentBytes;
            using (var binaryReader = new BinaryReader(file.InputStream))
                contentBytes = binaryReader.ReadBytes(file.ContentLength);

            if (!imageService.IsValidImage(contentBytes))
                return Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new MessageError(HttpStatusCode.NotAcceptable, Properties.Resources.FileNotImageMessage));

            Image image = AutoMapper.Map<byte[], Image>(contentBytes);
            OwningService.AddOwnership(User, image);
            imageService.Add(image);

            var imageUri = imageService.GetImageUri(Request.RequestUri, image.Id);
            return Request.CreateResponse(HttpStatusCode.Created, imageUri);
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Image deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            Image image = imageService.Get(id);
            if (image == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, image))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenError(Properties.Resources.ImageNotAuthorized));

            imageService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
