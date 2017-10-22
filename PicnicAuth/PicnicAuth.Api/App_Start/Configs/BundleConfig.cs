using System.Web.Optimization;

namespace PicnicAuth.Api.Configs
{
    /// <summary>
    /// Default class from ASP.NET project.
    /// Used to combine multiple resources into one file
    /// in order to reduce download time.
    /// 
    /// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// Registers js, css files into one bundle file.
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
