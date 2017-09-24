using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PicnicAuth.Database.Models.Authentication
{
    public class CompanyAccount : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CompanyAccount> manager,
            string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity companyIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return companyIdentity;
        }
    }
}