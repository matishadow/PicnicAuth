using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PicnicAuth.Models.Authentication.Identity
{
    public class Role : IdentityRole<Guid, CompanyAccountRole>
    {
        public Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}