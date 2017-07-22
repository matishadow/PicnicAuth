using System.Collections.Generic;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.ServiceInterfaces
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
