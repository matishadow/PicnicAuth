
using FriGo.Db.Models.Recipes;
using System.Collections.Generic;

namespace FriGo.ServiceInterfaces
{
    public interface ISearchEngine
    {
        IEnumerable<Recipe> RawData { get; }
        IEnumerable<Recipe> ProcessedRecipes { get; }

        void FilterByName(string nameSearchQuery);
        void FilterByTag(Tag[] tags);
        void SortByField(string field, bool descending);
    }
}
