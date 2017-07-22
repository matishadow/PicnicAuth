using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class SearchEngine : ISearchEngine
    {
        public IEnumerable<Recipe> RawData { get; set; }

        public IEnumerable<Recipe> ProcessedRecipes { get; set; }

        public SearchEngine(IList<Recipe> recipes)
        {
            RawData = recipes.ToList();
            ProcessedRecipes = recipes.ToList();
        }
        public void FilterByName(string nameSearchQuery)
        {
            if (nameSearchQuery != null)
                ProcessedRecipes = ProcessedRecipes.Where(x => x.Title.Contains(nameSearchQuery));
        }

        public void FilterByTag(Tag[] tags)
        {
            if(tags != null && tags.Any())
                ProcessedRecipes = ProcessedRecipes.Where(x => x.Tags.Select(y => y.Name)
                                     .Intersect(tags.Select(z => z.Name))
                                     .Any());
        }

        public void SortByField(string field, bool descending)
        {
            try
            {
                if (field == null) return;

                if (field == "Rating")
                {
                    ProcessedRecipes =
                        ProcessedRecipes.OrderBy(x => x.Rates.Select(rate => rate.Rating).Sum() /
                                                      (x.Rates.Count != 0 ? x.Rates.Count : 1));
                    return;
                }

                foreach (PropertyInfo property in typeof(Recipe).GetProperties())
                {
                    if (!string.Equals(field, property.Name, StringComparison.CurrentCultureIgnoreCase)) continue;

                    Func<Recipe, object> key = x => property.GetValue(x, null);
                    ProcessedRecipes = descending ? ProcessedRecipes.OrderByDescending(key) : ProcessedRecipes.OrderBy(key);
                }
            }
            catch (ArgumentException)
            {
                Type checkProperty = typeof(Recipe).GetProperty(field,
                      System.Reflection.BindingFlags.Instance |
                      System.Reflection.BindingFlags.Public |
                      System.Reflection.BindingFlags.NonPublic)
                    ?.PropertyType;

                if (checkProperty == typeof(ICollection<IngredientQuantity>))
                    ProcessedRecipes = SortByFitness();
            }
        }

        private IEnumerable<Recipe> SortByFitness()
        {
            throw new NotImplementedException();
        }
    }
}
