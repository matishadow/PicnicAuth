﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using PicnicAuth.Database;
using PicnicAuth.Models.Authentication;
using PicnicAuth.Models.Authentication.Identity;

namespace PicnicAuth.Api.Configs
{
    /// <inheritdoc />
    /// <summary>
    /// Configure the application user manager used in this application.CompanyManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public class CompanyManager : UserManager<CompanyAccount, Guid>
    {
        public CompanyManager(IUserStore<CompanyAccount, Guid> store)
            : base(store)
        {
        }

        public static CompanyManager Create(IdentityFactoryOptions<CompanyManager> options,
            IOwinContext context)
        {
            var manager = new CompanyManager(new CompanyAccountUserStore(context.Get<PicnicAuthContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<CompanyAccount, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 10,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
            IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<CompanyAccount, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
