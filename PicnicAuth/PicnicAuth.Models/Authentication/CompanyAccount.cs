using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicnicAuth.Models.Authentication.Identity;

namespace PicnicAuth.Models.Authentication
{
    public class CompanyAccount : IdentityUser<Guid, CompanyAccountLogin, CompanyAccountRole, CompanyAccountClaim>
    {
        public CompanyAccount()
        {
            Id = Guid.NewGuid();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CompanyAccount, Guid> manager,
            string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity companyIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return companyIdentity;
        }

        public virtual ICollection<AuthUser> AuthUsers { get; set; }
    }
}