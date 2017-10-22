using System;
using System.Net;
using PicnicAuth.Models;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Database.SwaggerResponses
{
    public class SwaggerOtpValidationDoneResponse : SwaggerResponseAttribute
    {
        public SwaggerOtpValidationDoneResponse(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public SwaggerOtpValidationDoneResponse() :
            base(HttpStatusCode.OK, Properties.Resources.SwaggerOtpValidationDoneMessage, typeof(OtpValidationResult))
        {
        }

        public SwaggerOtpValidationDoneResponse(int statusCode) : base(statusCode)
        {
        }

        public SwaggerOtpValidationDoneResponse(int statusCode, string description = null, Type type = null) : base(statusCode, description, type)
        {
        }
    }
}