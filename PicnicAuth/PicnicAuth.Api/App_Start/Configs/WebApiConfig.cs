using System.Net.Http.Headers;
using System.Web.Http;
using FluentValidation.WebApi;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using PicnicAuth.Api.Filters;

namespace PicnicAuth.Api.Configs
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            configuration.SuppressDefaultHostAuthentication();
            configuration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            configuration.Filters.Add(new ValidateModelStateFilter());

            configuration.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new DefaultContractResolver { IgnoreSerializableAttribute = true };

            // Web API routes
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            CorsConfig.ConfigureCors(configuration);

            FluentValidationModelValidatorProvider.Configure(configuration);
        }

       
    }
}
