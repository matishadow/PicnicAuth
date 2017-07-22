using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using AutoMapper;
using FluentValidation.Results;
using FriGo.Db.DTO.Ingredients;
using FriGo.Db.DTO.Units;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    public class IngredientController : BaseFriGoController
    {
        private readonly IIngredientService ingredientService;
        private readonly IUnitService unitService;
        private readonly IInputIngredientValidator ingredientValidator;

        public IngredientController(IMapper autoMapper, IValidatingService validatingService,
            IOwningService owningService, IIngredientService ingredientService, IUnitService unitService,
            IInputIngredientValidator ingredientValidator) : base(autoMapper, validatingService, owningService)
        {
            this.ingredientService = ingredientService;
            this.unitService = unitService;
            this.ingredientValidator = ingredientValidator;
        }

        /// <summary>
        /// Returns all ingredients
        /// </summary>
        /// <returns>An array of ingredients</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<IngredientDto>))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get()
        {
            IEnumerable<Ingredient> ingredients = ingredientService.Get();
            IEnumerable<IngredientDto> ingredientDtos =
                AutoMapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientDto>>(ingredients);

            return Request.CreateResponse(HttpStatusCode.OK, ingredientDtos.OrderBy(ingredient => ingredient.Name));
        }

        /// <summary>
        /// Get one ingredient by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One ingredient</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientDto))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get(Guid id)
        {
            Ingredient ingredient = ingredientService.Get(id);
            IngredientDto ingredientDto = AutoMapper.Map<Ingredient, IngredientDto>(ingredient);

            return Request.CreateResponse(HttpStatusCode.OK, ingredientDto);
        }

        /// <summary>
        /// Create new ingredient
        /// </summary>
        /// <param name="createIngredient"></param>
        /// <returns>Created ingredient</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(IngredientDto), Description = "Ingredient created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        [SwaggerResponse(HttpStatusCode.Accepted, Type = typeof(ModelState), Description = "Provided data was not valid")]
        public virtual HttpResponseMessage Post(CreateIngredient createIngredient)
        {
            if (!ValidatingService.IsValid(ingredientValidator, createIngredient))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(ingredientValidator, createIngredient));

            Ingredient ingredient = AutoMapper.Map<CreateIngredient, Ingredient>(createIngredient);
            OwningService.AddOwnership(User, ingredient);
            ingredientService.Add(ingredient);

            Ingredient createdIngredient = ingredientService.Get(ingredient.Id);
            IngredientDto createdIngredientDto = AutoMapper.Map<Ingredient, IngredientDto>(createdIngredient);

            return Request.CreateResponse(HttpStatusCode.Created, createdIngredientDto);
        }

        /// <summary>
        /// Modify existing ingredient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editIngredient"></param>
        /// <returns>Modified ingredient</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientDto), Description = "Ingredient updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Put(Guid id, EditIngredient editIngredient)
        {
            if (!ValidatingService.IsValid(ingredientValidator, editIngredient))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(ingredientValidator, editIngredient));

            Ingredient ingredient = ingredientService.Get(id);
            if (ingredient == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, ingredient))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenPutError());

            AutoMapper.Map(editIngredient, ingredient);
            ingredientService.Edit(ingredient);

            IngredientDto ingredientDto = AutoMapper.Map<Ingredient, IngredientDto>(ingredient);
            return Request.CreateResponse(HttpStatusCode.OK, ingredientDto);
        }

        /// <summary>
        /// Delete ingredient
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Ingredient deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            Ingredient ingredient = ingredientService.Get(id);
            if (ingredient == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, ingredient))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenDeleteError());

            ingredientService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
