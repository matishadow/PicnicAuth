using System.Web.Http.Controllers;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Interfaces.Web
{
    public interface ILoggedCompanyGetter
    {
        CompanyAccount GetLoggedCompany(HttpRequestContext httpRequestContext);
    }
}