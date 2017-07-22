using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using PicnicAuth.Database.Models;

namespace PicnicAuth.Api.Filters
{
    public class PicnicAuthAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            const HttpStatusCode unauthorizedStatusCode = HttpStatusCode.Unauthorized;
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = unauthorizedStatusCode,
                Content = new ObjectContent<MessageError>(
                    new MessageError(unauthorizedStatusCode, Properties.Resources.PicnicAuthUnauthorizedMessage),
                    new JsonMediaTypeFormatter(), Properties.Resources.JsonMediaHeader)
            };
        }
    }
}