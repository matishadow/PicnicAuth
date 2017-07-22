using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DTO.IngredientQuantities;
using PicnicAuth.Database.DTO.RecipeNotes;
using PicnicAuth.Database.DTO.Recipes;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.Database.Models.Social;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;
using WebGrease.Css.Extensions;

namespace PicnicAuth.Api.Controllers
{
    public class RecipeController : BasePicnicAuthController
    {
        private readonly IRecipeService recipeService;
        private readonly IUserService userService;
        private readonly IFitnessService fitnessService;
        private readonly IRecipeNoteService recipeNoteService;
        private readonly IImageService imageService;
        private readonly ICommentService commentService;

        private readonly ICreateRecipeValidator createRecipeValidator;

        public RecipeController(IMapper autoMapper, IValidatingService validatingService, IOwningService owningService,
            IRecipeService recipeService, IUserService userService, IFitnessService fitnessService,
            IRecipeNoteService recipeNoteService, ICreateRecipeValidator createRecipeValidator,
            IImageService imageService, ICommentService commentService) : base(autoMapper,
            validatingService, owningService)
        {
            this.recipeService = recipeService;
            this.userService = userService;
            this.fitnessService = fitnessService;
            this.recipeNoteService = recipeNoteService;
            this.createRecipeValidator = createRecipeValidator;
            this.imageService = imageService;
            this.commentService = commentService;
        }

        /// <summary>
        /// Get one recipe by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One type of unit</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RecipeDto))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get(Guid id)
        {
            Recipe recipe = recipeService.Get(id);
            RecipeDto recipeDto = AutoMapper.Map<Recipe, RecipeDto>(recipe);

            recipeDto = CompleteMissingFields(recipeDto, recipe);

            return Request.CreateResponse(HttpStatusCode.OK, recipeDto);
        }

        /// <summary>
        /// Get recipe by parameters
        /// </summary>
        /// <param name="page">Number of page</param>
        /// <param name="perPage">Count per page</param>
        /// <param name="sortField">Sorting by field</param>
        /// <param name="fitness">Sorting by fitness</param>
        /// <param name="descending">Sorting order</param>
        /// <param name="nameSearchQuery">Search by name</param>
        /// <param name="tagQuery">Search by tags</param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RecipeDto))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get([FromUri] string[] tagQuery = null, int page = 1, int perPage = 10,
            string sortField = null, decimal fitness = 0,
            bool descending = false, string nameSearchQuery = null)
        {
            Tag[] tags = AutoMapper.Map<string[], Tag[]>(tagQuery);
            recipeService.Engine.FilterByName(nameSearchQuery);
            recipeService.Engine.FilterByTag(tags);
            recipeService.Engine.SortByField(sortField, descending);

            IList<Recipe> recipes
                = recipeService.Engine.ProcessedRecipes
                    .Skip((page - 1) * perPage).Take(perPage).ToList();

            if (!recipes.Any()) return Request.CreateResponse(HttpStatusCode.NoContent);

            IList<RecipeDto> recipeDtos = AutoMapper
                .Map<IList<Recipe>, IList<RecipeDto>>(recipes);

            IList<RecipeDto> completedRecipeDtos = new List<RecipeDto>();
            for (int i = 0; i < recipeDtos.Count; i++)
            {
                RecipeDto recipeDto = recipeDtos[i];
                Recipe recipe = recipes[i];

                completedRecipeDtos.Add(CompleteMissingFields(recipeDto, recipe));
            }
            completedRecipeDtos = fitnessService.SortByFitness(fitness, recipeDtos).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, completedRecipeDtos);
        }

        /// <summary>
        /// Create new recipe
        /// </summary>
        /// <param name="createRecipe"></param>
        /// <returns>Created unit</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(RecipeStub), Description = "Recipe created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        public HttpResponseMessage Post(CreateRecipe createRecipe)
        {
            if (!ValidatingService.IsValid(createRecipeValidator, createRecipe))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(createRecipeValidator, createRecipe));
        

            Recipe newRecipe = AutoMapper.Map<CreateRecipe, Recipe>(createRecipe);
            if (newRecipe.ImageId == null)
                recipeService.SetDefaultPicture(newRecipe);

            string uid = User.Identity.GetUserId();
            User user = userService.Get(uid);
            newRecipe.User = user;
            OwningService.AddOwnership(User, newRecipe);

            recipeService.Add(newRecipe);
            Recipe recipe = recipeService.Get(newRecipe.Id);
            RecipeStub recipeStub = AutoMapper.Map<Recipe, RecipeStub>(recipe);

            return Request.CreateResponse(HttpStatusCode.Created, recipeStub);
        }

        /// <summary>
        /// Delete recipe
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Recipe deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            Recipe recipe = recipeService.Get(id);
            if (recipe == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, recipe))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenDeleteError());

            if (recipe.Comments != null && recipe.Comments.Any())
            {
                IList<Comment> comments = recipe.Comments.ToList();
                comments.ForEach(comment => commentService.Delete(comment.Id));
            }

            recipeService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        private RecipeDto CompleteMissingFields(RecipeDto recipeDto, Recipe recipe)
        {
            if (recipeDto == null) return null;

            recipeDto.Rating = recipeService.GetRatingByRecipe(recipe);

            string userId = User.Identity.GetUserId();
            User user = userService.Get(userId);
            if (user != null)
            {
                IEnumerable<RecipeNote> recipeNote = recipeNoteService.Get(recipe.Id, new Guid(User.Identity.GetUserId()));
                recipeDto.Notes = AutoMapper.Map<IEnumerable<RecipeNote>, IEnumerable<RecipeNoteDto>>(recipeNote);

                recipeDto.UserRating = (int?)recipeService.GetRatingByUser(userId, recipe.Id);
                recipeDto.Fitness =
                    fitnessService.CalculateFitness(recipe, user.IngredientQuantities?.ToList());

                IEnumerable<IngredientQuantity> missingIngredientQuantities =
                    fitnessService.GetMissingIngredientQuantities(recipe, user.IngredientQuantities?.ToList());
                recipeDto.MissingIngredientQuantities = AutoMapper
                    .Map<IEnumerable<IngredientQuantity>, IEnumerable<IngredientQuantityDto>>(
                        missingIngredientQuantities);
            }
            else
            {
                recipeDto.Notes = new List<RecipeNoteDto>();
                recipeDto.MissingIngredientQuantities = new List<IngredientQuantityDto>();
            }

            if (recipe.ImageId != null)
                recipeDto.ImageUrl = imageService.GetImageUri(Request.RequestUri, recipe.ImageId);

            return recipeDto;
        }
    }
}