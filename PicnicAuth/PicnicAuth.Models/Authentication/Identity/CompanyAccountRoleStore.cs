using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PicnicAuth.Models.Authentication.Identity
{
    public class CompanyAccountRoleStore : RoleStore<Role, Guid, CompanyAccountRole>
    {
        public CompanyAccountRoleStore(DbContext context) : base(context)
        {
        }
    }
}