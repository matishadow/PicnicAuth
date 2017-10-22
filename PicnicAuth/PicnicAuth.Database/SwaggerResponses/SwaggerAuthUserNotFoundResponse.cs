using System;
using System.Net;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Database.SwaggerResponses
{
    public class SwaggerAuthUserNotFoundResponse : SwaggerResponseAttribute
    {
        public SwaggerAuthUserNotFoundResponse(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public SwaggerAuthUserNotFoundResponse(Type type = null) : 
            base(HttpStatusCode.NotFound, Properties.Resources.SwaggerAuthUserNotFoundMessage, type)
        {
        }

        public SwaggerAuthUserNotFoundResponse(int statusCode) : base(statusCode)
        {
        }

        public SwaggerAuthUserNotFoundResponse(int statusCode, string description = null, Type type = null) : base(statusCode, description, type)
        {
        }
    }
}