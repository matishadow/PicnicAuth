using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PicnicAuth.Models.Authentication.Identity
{
    public class CompanyAccountUserStore : UserStore<CompanyAccount, Role, Guid, CompanyAccountLogin, 
        CompanyAccountRole, CompanyAccountClaim>
    {
        public CompanyAccountUserStore(DbContext context) : base(context)
        {
        }
    }
}