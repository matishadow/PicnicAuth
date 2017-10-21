using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Web;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Implementations.Web
{
    public class LoggedCompanyGetter : ILoggedCompanyGetter, IRequestDependency
    {
        private readonly IUnitOfWork unitOfWork;

        public LoggedCompanyGetter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public CompanyAccount GetLoggedCompany(HttpRequestContext httpRequestContext)
        {
            IGenericRepository<CompanyAccount> companyRepository = unitOfWork.Repository<CompanyAccount>();
            string loggedCompanyId = httpRequestContext.Principal.Identity.GetUserId();
            CompanyAccount loggedCompany = companyRepository.GetById(loggedCompanyId);

            return loggedCompany;
        }
    }
}