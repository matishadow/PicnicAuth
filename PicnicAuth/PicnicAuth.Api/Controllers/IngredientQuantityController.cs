using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DTO.IngredientQuantities;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    public class IngredientQuantityController : BasePicnicAuthController
    {
        private readonly IIngredientQuantityService ingredientQuantityService;
        private readonly IUserService userService;
        private readonly IIngredientService ingredientService;

        private readonly ICreateIngredientQuantityValidator createIngredientQuantityValidator;
        private readonly IEditIngredientQuantityValidator editIngredientQuantityValidator;

        public IngredientQuantityController(IMapper autoMapper, IValidatingService validatingService,
            IOwningService owningService, IIngredientQuantityService ingredientQuantityService,
            IUserService userService, IIngredientService ingredientService,
            ICreateIngredientQuantityValidator createIngredientQuantityValidator,
            IEditIngredientQuantityValidator editIngredientQuantityValidator) : base(autoMapper, validatingService,
            owningService)
        {
            this.ingredientQuantityService = ingredientQuantityService;
            this.userService = userService;
            this.ingredientService = ingredientService;
            this.createIngredientQuantityValidator = createIngredientQuantityValidator;
            this.editIngredientQuantityValidator = editIngredientQuantityValidator;
        }

        /// <summary>
        /// Returns user's ingredients with quantities
        /// </summary>
        /// <returns>An array of all ingredients with quantities</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<IngredientQuantityDto>), Description = "Returns ingredients in user\'s fridge")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public HttpResponseMessage Get()
        {
            string userId = User.Identity.GetUserId();

            IEnumerable<IngredientQuantity> ingredientQuantities = ingredientQuantityService.GetByUserId(userId);
            IEnumerable<IngredientQuantityDto> ingredientQuantityDtos =
                AutoMapper.Map<IEnumerable<IngredientQuantity>, IEnumerable<IngredientQuantityDto>>(
                    ingredientQuantities);

            return Request.CreateResponse(HttpStatusCode.OK, ingredientQuantityDtos);
        }

        /// <summary>
        /// Returns one ingredient with quantity by Id
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientQuantityDto), Description = "Return an ingredient in user's fridge with specified id")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public HttpResponseMessage Get(Guid id)
        {
            string userId = User.Identity.GetUserId();

            IngredientQuantity ingredientQuantity = ingredientQuantityService.Get(userId, id);
            IngredientQuantityDto ingredientQuantityDto =
                AutoMapper.Map<IngredientQuantity, IngredientQuantityDto>(ingredientQuantity);

            return Request.CreateResponse(HttpStatusCode.OK, ingredientQuantityDto);
        }

        /// <summary>
        /// Adds ingredient to user's fridge
        /// </summary>
        /// <param name="createIngredientQuantity"></param>
        /// <returns>Created ingredient with quantity</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(IngredientQuantityDto), Description = "Ingredient quantity created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        public HttpResponseMessage Post(CreateIngredientQuantity createIngredientQuantity)
        {
            if (!ValidatingService.IsValid(createIngredientQuantityValidator, createIngredientQuantity))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(createIngredientQuantityValidator, createIngredientQuantity));

            string userId = User.Identity.GetUserId();
            User user =  userService.Get(userId);

            Ingredient ingredient = ingredientService.Get(createIngredientQuantity.IngredientId);

            IngredientQuantity ingredientQuantity = AutoMapper.Map<CreateIngredientQuantity, IngredientQuantity>(createIngredientQuantity);
            ingredientQuantity.Ingredient = ingredient;

            user.IngredientQuantities.Add(ingredientQuantity);

            userService.Edit(user);

            IngredientQuantityDto ingredientQuantityDto =
                AutoMapper.Map<IngredientQuantity, IngredientQuantityDto>(ingredientQuantity);
            return Request.CreateResponse(HttpStatusCode.Created, ingredientQuantityDto);
        }

        /// <summary>
        /// Modify existing ingredient quantity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editIngredientQuantity"></param>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientQuantityDto), Description = "Ingredient quantity updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        public HttpResponseMessage Put(Guid id, EditIngredientQuantity editIngredientQuantity)
        {
            if (!ValidatingService.IsValid(editIngredientQuantityValidator, editIngredientQuantity))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(editIngredientQuantityValidator, editIngredientQuantity));

            IngredientQuantity ingredientQuantity = ingredientQuantityService.Get(id);
            if (ingredientQuantity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            string userId = User.Identity.GetUserId();
            if (!ingredientQuantityService.IsOwnedByUser(userId, id))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    new MessageError(HttpStatusCode.Forbidden,
                        Properties.Resources.IngredientQuantityPutForbiddenMessage));

            AutoMapper.Map(editIngredientQuantity, ingredientQuantity);

            ingredientQuantityService.Edit(ingredientQuantity);
            IngredientQuantityDto ingredientQuantityDto =
                AutoMapper.Map<IngredientQuantity, IngredientQuantityDto>(ingredientQuantity);

            return Request.CreateResponse(HttpStatusCode.OK, ingredientQuantityDto);
        }

        /// <summary>
        /// Delete ingredient quantity
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Ingredient quantity deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public HttpResponseMessage Delete(Guid id)
        {
            IngredientQuantity ingredientQuantity = ingredientQuantityService.Get(id);
            if (ingredientQuantity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            string userId = User.Identity.GetUserId();
            if (!ingredientQuantityService.IsOwnedByUser(userId, id))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    new MessageError(HttpStatusCode.Forbidden,
                        Properties.Resources.IngredientQuantityDeleteForbiddenMessage));

            ingredientQuantityService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
