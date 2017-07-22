using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AutoMapper;
using FriGo.Db.DTO.RecipeNotes;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models;
using FriGo.Db.Models.Recipes;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.ServiceInterfaces;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    public class RecipeNoteController : BaseFriGoController
    {
        private readonly IRecipeNoteService recipeNoteService;

        private readonly ICreateRecipeNoteValidator createRecipeNoteValidator;
        private readonly IEditRecipeNoteValidator editRecipeNoteValidator;

        public RecipeNoteController(IMapper autoMapper, IValidatingService validatingService,
            IOwningService owningService, IRecipeNoteService recipeNoteService,
            IEditRecipeNoteValidator editRecipeNoteValidator,
            ICreateRecipeNoteValidator createRecipeNoteValidator) : base(autoMapper, validatingService,
            owningService)
        {
            this.recipeNoteService = recipeNoteService;
            this.editRecipeNoteValidator = editRecipeNoteValidator;
            this.createRecipeNoteValidator = createRecipeNoteValidator;
        }

        /// <summary>
        /// Returns all notes
        /// </summary>
        /// <returns>An array of user's notes</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<RecipeNoteDto>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        public virtual HttpResponseMessage Get()
        {
            IEnumerable<RecipeNote> recipeNotes = recipeNoteService.Get();
            recipeNotes = recipeNotes.Where(recipeNote => recipeNote.OwnerId == new Guid(User.Identity.GetUserId()));

            IEnumerable<RecipeNoteDto> recipeNoteDtos =
                AutoMapper.Map<IEnumerable<RecipeNote>, IEnumerable<RecipeNoteDto>>(recipeNotes);
            return Request.CreateResponse(HttpStatusCode.OK, recipeNoteDtos);
        }

        /// <summary>
        /// Get one note by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One note</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RecipeNoteDto))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        public virtual HttpResponseMessage Get(Guid id)
        {
            RecipeNote recipeNote = recipeNoteService.Get(id);

            if (recipeNote == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            RecipeNoteDto recipeNoteDto = AutoMapper.Map<RecipeNote, RecipeNoteDto>(recipeNote);
            if (OwningService.IsOwnedByUser(User, recipeNote))
                return Request.CreateResponse(HttpStatusCode.OK, recipeNoteDto);

            return Request.CreateResponse(HttpStatusCode.Forbidden,
                OwningService.GenerateForbiddenError(Properties.Resources.RecipeNoteNotOwnedGetMessage));
        }

        /// <summary>
        /// Create new note for given recipe
        /// </summary>
        /// <param name="createRecipeNote"></param>
        /// <returns>Created ingredient</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(RecipeNoteDto), Description = "Note created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        public virtual HttpResponseMessage Post(CreateRecipeNote createRecipeNote)
        {
            if (!ValidatingService.IsValid(createRecipeNoteValidator, createRecipeNote))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(createRecipeNoteValidator, createRecipeNote));

            RecipeNote recipeNote = AutoMapper.Map<CreateRecipeNote, RecipeNote>(createRecipeNote);
            OwningService.AddOwnership(User, recipeNote);
            recipeNoteService.Add(recipeNote);

            RecipeNoteDto recipeNoteDto = AutoMapper.Map<RecipeNote, RecipeNoteDto>(recipeNote);
            return Request.CreateResponse(HttpStatusCode.Created, recipeNoteDto);
        }

        /// <summary>
        /// Modify existing text in a note
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editRecipeNote"></param>
        /// <returns>Modified note</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RecipeNoteDto), Description = "Recipe note updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "You can edit only your notes")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Put(Guid id, EditRecipeNote editRecipeNote)
        {
            if (!ValidatingService.IsValid(editRecipeNoteValidator, editRecipeNote))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(editRecipeNoteValidator, editRecipeNote));

            RecipeNote recipeNote = recipeNoteService.Get(id);

            if (recipeNote == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound,
                        Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, recipeNote))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenPutError());

            AutoMapper.Map(editRecipeNote, recipeNote);

            recipeNoteService.Edit(recipeNote);

            RecipeNoteDto recipeNoteDto = AutoMapper.Map<RecipeNote, RecipeNoteDto>(recipeNote);
            return Request.CreateResponse(HttpStatusCode.OK, recipeNoteDto);
        }

        /// <summary>
        /// Delete note
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Note deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "You can remove only your notes")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            RecipeNote recipeNote = recipeNoteService.Get(id);
            if (recipeNote == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound,
                        Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, recipeNote))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenDeleteError());

            recipeNoteService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
