using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DTO.Recipes;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    public class RateController : BasePicnicAuthController
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
