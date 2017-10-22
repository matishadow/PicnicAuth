using System.Web.Http;
using System.Web.Http.Cors;

namespace PicnicAuth.Api.Configs
{
    /// <summary>
    /// Need to enable CORS to be able to send request 
    /// between this API and some other websites.
    /// 
    /// Some readings here: https://en.wikipedia.org/wiki/Cross-origin_resource_sharing
    /// </summary>
    public static class CorsConfig
    {
        /// <summary>
        /// Change wildcards into appropriate tokens.
        /// </summary>
        /// <param name="configuration"></param>
        public static void ConfigureCors(HttpConfiguration configuration)
        {
            string corsWildcard = Properties.Resources.CorsAllowAllWildcard;
            var cors = new EnableCorsAttribute(corsWildcard, corsWildcard, corsWildcard);
            configuration.EnableCors(cors);
        }
    }
}