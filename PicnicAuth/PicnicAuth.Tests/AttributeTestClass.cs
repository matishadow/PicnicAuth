using System;
using System.Net;
using PicnicAuth.Database.SwaggerResponses;

namespace PicnicAuth.Tests
{
    [SwaggerOtpValidationDoneResponse(HttpStatusCode.Accepted)]
    [SwaggerOtpValidationDoneResponse(200)]
    [SwaggerOtpValidationDoneResponse()]
    [SwaggerOtpValidationDoneResponse(200, "aa", typeof(string))]
    [SwaggerCompanyNotLoggedInResponse(HttpStatusCode.Accepted)]
    [SwaggerCompanyNotLoggedInResponse(200)]
    [SwaggerCompanyNotLoggedInResponse()]
    [SwaggerCompanyNotLoggedInResponse(200, "aa", typeof(string))]
    [SwaggerAuthUserNotFoundResponse(HttpStatusCode.Accepted)]
    [SwaggerAuthUserNotFoundResponse(200)]
    [SwaggerAuthUserNotFoundResponse()]
    [SwaggerAuthUserNotFoundResponse(200, "aa", typeof(string))]
    public class AttributeTestClass
    {
        
    }
}