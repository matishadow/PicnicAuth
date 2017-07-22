using FriGo.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FriGo.Api.Filters
{
    public class FrigoAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            const HttpStatusCode unauthorizedStatusCode = HttpStatusCode.Unauthorized;
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = unauthorizedStatusCode,
                Content = new ObjectContent<MessageError>(
                    new MessageError(unauthorizedStatusCode, Properties.Resources.FrigoUnauthorizedMessage),
                    new JsonMediaTypeFormatter(), Properties.Resources.JsonMediaHeader)
            };
        }
    }
}