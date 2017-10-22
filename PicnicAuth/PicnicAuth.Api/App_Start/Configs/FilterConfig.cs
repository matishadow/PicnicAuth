using System.Web.Mvc;

namespace PicnicAuth.Api.Configs
{
    /// <summary>
    /// Out of the box ASP.NET project class.
    /// </summary>
    public static class FilterConfig
    {
        /// <summary>
        /// Use this method to register filter which are applied to each request.
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
