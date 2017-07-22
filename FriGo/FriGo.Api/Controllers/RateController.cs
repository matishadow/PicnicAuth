using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;
using Microsoft.AspNet.Identity;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Recipes;
using System.Linq;
using FriGo.Db.ModelValidators.Interfaces;

namespace FriGo.Api.Controllers
{
    public class RateController : BaseFriGoController
    {
        private readonly IRateService rateService;
        private readonly IRecipeService recipeService;
        private readonly IUserService userService;

        private readonly IRateValidator rateValidator;

        public RateController(IMapper autoMapper, IValidatingService validatingService, IOwningService owningService,
            IRateService rateService, IRecipeService recipeService, IUserService userService,
            IRateValidator rateValidator) : base(autoMapper,
            validatingService, owningService)
        {
            this.rateService = rateService;
            this.recipeService = recipeService;
            this.userService = userService;
            this.rateValidator = rateValidator;
        }

        /// <summary>
        /// Rate recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="rateRecipe"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RateRecipe), Description = "Rate")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Put(Guid recipeId, RateRecipe rateRecipe)
        {
            if (!ValidatingService.IsValid(rateValidator, rateRecipe))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(rateValidator, rateRecipe));

            User user = userService.Get(User.Identity.GetUserId());
            Recipe recipe = recipeService.Get(recipeId);
            if (recipe == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            decimal? ratesForUser = recipeService.GetRatingByUser(user.Id, recipe.Id);
            if (ratesForUser == null)
            {
                Rate rate = AutoMapper.Map<RateRecipe, Rate>(rateRecipe);
                rate.Recipe = recipe;
                rate.User = user;
                OwningService.AddOwnership(User, rate);

                recipe.Rates.Add(rate);
                recipeService.Edit(recipe);

                return Request.CreateResponse(HttpStatusCode.OK, rateRecipe);
            }

            IEnumerable<Rate> ratesById = rateService.GetByRecipeId(recipeId);
            Rate rateById = ratesById.FirstOrDefault(rate => rate.User.Id == User.Identity.GetUserId());
            if (rateById == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!OwningService.IsOwnedByUser(User, rateById))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenPutError());

            rateById.Rating = rateRecipe.Rate;
            rateService.Edit(rateById);
            return Request.CreateResponse(HttpStatusCode.OK, rateRecipe);
        }
    }
}
