using System;
using System.Net;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Database.SwaggerResponses
{
    public class SwaggerCompanyNotLoggedInResponse : SwaggerResponseAttribute
    {
        public SwaggerCompanyNotLoggedInResponse(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public SwaggerCompanyNotLoggedInResponse(Type type = null) : 
            base(HttpStatusCode.Unauthorized, Properties.Resources.SwaggerCompanyNotLoggedInMessage, type)
        {
        }

        public SwaggerCompanyNotLoggedInResponse(int statusCode) : base(statusCode)
        {
        }

        public SwaggerCompanyNotLoggedInResponse(int statusCode, string description = null, Type type = null) : base(statusCode, description, type)
        {
        }
    }
}