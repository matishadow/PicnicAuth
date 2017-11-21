using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Identity.EntityFramework;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Models;
using PicnicAuth.Models.Authentication;
using PicnicAuth.Models.Authentication.Identity;

namespace PicnicAuth.Database
{
    public class PicnicAuthContext :
        IdentityDbContext<CompanyAccount, Role, Guid, CompanyAccountLogin, CompanyAccountRole, CompanyAccountClaim>,
        ISelfRequestDependency
    {
        public PicnicAuthContext() : base("name=picnicauthdb")
        {
            System.Data.Entity.Database.SetInitializer(new PicnicAuthDatabaseInitializer());
        }

        public static PicnicAuthContext Create()
        {
            return new PicnicAuthContext();
        }

        private IEnumerable<Type> GetEntityConfigurationTypes(Assembly assembly)
        {
            IEnumerable<Type> configTypes = assembly
                .GetTypes()
                .Where(t => t.BaseType != null
                            && t.BaseType.IsGenericType
                            && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            return configTypes;
        }

        private MethodInfo GetEntityConfigurationAddMethod()
        {
            MethodInfo addMethod = typeof(ConfigurationRegistrar)
                .GetMethods()
                .Single(m =>
                    m.Name == "Add"
                    && m.GetGenericArguments().Any(a => a.Name == "TEntityType"));

            return addMethod;
        }

        private void RegiterEntityConfigurationTypes(DbModelBuilder modelBuilder, IEnumerable<Type> configurationTypes,
            Assembly assembly, MethodInfo addMethod)
        {
            foreach (Type type in configurationTypes)
            {
                Type entityType = type?.BaseType?.GetGenericArguments().Single();

                object entityConfig = assembly.CreateInstance(type.FullName);
                addMethod.MakeGenericMethod(entityType)
                    .Invoke(modelBuilder.Configurations, new[] { entityConfig });
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MethodInfo addMethod = GetEntityConfigurationAddMethod();
            Assembly assembly = Assembly.GetAssembly(typeof(Entity));
            IEnumerable<Type> configurationTypes = GetEntityConfigurationTypes(assembly);

            RegiterEntityConfigurationTypes(modelBuilder, configurationTypes, assembly, addMethod);

            base.OnModelCreating(modelBuilder);
        }     
    }
}
