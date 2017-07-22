using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DTO.Social;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.Database.Models.Social;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    public class CommentController : BasePicnicAuthController
    {
        private readonly ICommentService commentService;
        private readonly IRecipeService recipeService;
        private readonly IUserService userService;
        private readonly IImageService imageService;

        private readonly IInputCommentValidator inputCommentValidator;

        public CommentController(IMapper autoMapper, IValidatingService validatingService, IOwningService owningService,
            ICommentService commentService, IRecipeService recipeService,
            IInputCommentValidator inputCommentValidator, IImageService imageService, IUserService userService) : base(
            autoMapper, validatingService,
            owningService)
        {
            this.commentService = commentService;
            this.recipeService = recipeService;
            this.inputCommentValidator = inputCommentValidator;
            this.imageService = imageService;
            this.userService = userService;
        }

        /// <summary>
        /// Get comments of recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>Rating of a recipe</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<CommentDto>))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Not found")]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get(Guid recipeId)
        {
            Recipe recipe = recipeService.Get(recipeId);
            if (recipe == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            ICollection<CommentDto> commentDto = AutoMapper.Map<ICollection<Comment>, ICollection<CommentDto>>(recipe.Comments);

            return Request.CreateResponse(HttpStatusCode.OK, commentDto);
        }

        /// <summary>
        /// Edit comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editComment"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CommentDto), Description = "Comment updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Put(Guid id, EditComment editComment)
        {
            Comment comment = commentService.Get(id);
            if (comment == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, comment))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenPutError());

            if (!ValidatingService.IsValid(inputCommentValidator, editComment))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(inputCommentValidator, editComment));

            comment.Text = editComment.Text;
            commentService.Edit(comment);
            CommentDto commentDto = AutoMapper.Map<Comment, CommentDto>(comment);
            if (comment.ImageId != null)
                commentDto.ImageUrl = imageService.GetImageUri(Request.RequestUri, comment.ImageId);
            return Request.CreateResponse(HttpStatusCode.OK, commentDto);
        }

        /// <summary>
        /// Comment recipe
        /// </summary>
        /// <param name="createComment"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(CommentDto), Description = "Comment created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        public virtual HttpResponseMessage Post(CreateComment createComment)
        {
            Recipe recipe = recipeService.Get(createComment.RecipeId);
            if (recipe == null) 
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!ValidatingService.IsValid(inputCommentValidator, createComment))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(inputCommentValidator, createComment));

            Comment comment = AutoMapper.Map<CreateComment, Comment>(createComment);
            User currentUser = userService.Get(User.Identity.GetUserId());
            comment.User = currentUser;
            OwningService.AddOwnership(User, comment);
            recipe.Comments.Add(comment);

            recipeService.Edit(recipe);
            comment = commentService.Get(comment.Id);
            CommentDto commentDto = AutoMapper.Map<Comment, CommentDto>(comment);
            if (comment.ImageId != null)
                commentDto.ImageUrl = imageService.GetImageUri(Request.RequestUri, comment.ImageId);
            return Request.CreateResponse(HttpStatusCode.Created, commentDto);
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Comment deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            Comment comment = commentService.Get(id);
            if (comment == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, comment))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenDeleteError());

            commentService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);

        }
    }
}
