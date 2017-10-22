using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Swashbuckle.Application;

namespace PicnicAuth.Api.Configs
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                "swagger_root",
                string.Empty,
                null,
                null,
                new RedirectHandler(message => message.RequestUri.ToString(), "swagger"));

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}
