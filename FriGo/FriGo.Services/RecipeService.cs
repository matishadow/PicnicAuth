using System;
using System.Collections.Generic;
using System.Linq;
using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.Db.DAL;
using FriGo.ServiceInterfaces;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Ingredients;

namespace FriGo.Services

{
    public class RecipeService : CrudService<Recipe>, IRecipeService, IRequestDependency
    {
        public ISearchEngine Engine { get; set; }
        private readonly IRateService rateService;
        private readonly ITagService tagService;

        public RecipeService(IUnitOfWork unitOfWork, IRateService rateService,
            ITagService tagService) : base(unitOfWork)
        {
            Engine = new SearchEngine(Get().ToList());
            this.rateService = rateService;
            this.tagService = tagService;
        }

        public override void Add(Recipe recipe)
        {
            if (recipe.Tags != null)
            {
                IEnumerable<Tag> tags = recipe.Tags.GroupBy(tag => tag.Name).Select(group => group.First()).ToList();
                IList<Tag> tagsToInsert = new List<Tag>();

                foreach (Tag tag in tags)
                {
                    Tag databaseTag = tagService.Get()
                        .FirstOrDefault(t => string.Equals(t.Name, tag.Name,
                            StringComparison.CurrentCultureIgnoreCase));
                    tagsToInsert.Add(databaseTag ?? tag);
                }

                recipe.Tags = tagsToInsert;
            } 

            base.Add(recipe);
        }

        public void SetDefaultPicture(Recipe recipe)
        {
            if (recipe == null) return;
            if (recipe.Tags == null)
            {
                recipe.ImageId = new Guid(Db.Properties.Resources.MainCourseImageId);
                return;
            }

            IList<string> stringTags = GetStringTags(recipe);
            IList<string> dessertTags = GetDessertTags();
            IList<string> appetizerTags = GetAppetizerTags();

            if (stringTags.Any(tag => dessertTags.Contains(tag)))
                recipe.ImageId = new Guid(Db.Properties.Resources.DessertImageId);
            else if (stringTags.Any(tag => appetizerTags.Contains(tag)))
                recipe.ImageId = new Guid(Db.Properties.Resources.AppetizerImageId);
            else
                recipe.ImageId = new Guid(Db.Properties.Resources.MainCourseImageId);
        }

        public decimal? GetRatingByRecipe(Recipe recipe)
        {
            IEnumerable<Rate> rates = rateService.GetByRecipeId(recipe.Id).ToList();
            if (!rates.Any()) return null;

            decimal rating = rates.Select(rate => rate.Rating).Sum() / Convert.ToDecimal(rates.Count());
            return rating;
        }

        public decimal? GetRatingByUser(string userId, Guid recipeId)
        {
            IEnumerable<Rate> rates = rateService.GetByRecipeId(recipeId).ToList();

            if (!rates.Any()) return null;

            Rate rateForUser = rates.FirstOrDefault(rate => rate.User.Id == userId);
            return rateForUser?.Rating;
        }

        private IList<string> GetStringTags(Recipe recipe)
        {
            return recipe?.Tags?.Select(tag => tag.Name).ToList() ?? new List<string>();
        }

        private IList<string> GetDessertTags()
        {
            return Db.Properties.Resources.DessertTags.Split(new[] { Db.Properties.Resources.TagsDelimiter },
                StringSplitOptions.None);
        }

        private IList<string> GetAppetizerTags()
        {
            return Db.Properties.Resources.AppetizerTags.Split(new[] { Db.Properties.Resources.TagsDelimiter },
                StringSplitOptions.None);
        }
    }
}