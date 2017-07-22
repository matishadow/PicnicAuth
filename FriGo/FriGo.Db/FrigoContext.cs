using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using FriGo.Db.EntityConfigurations;
using FriGo.Db.Models;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Social;
using FriGo.Interfaces.Dependencies;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FriGo.Db
{
    public class FrigoContext : IdentityDbContext<User, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, ISelfRequestDependency
    {
        public FrigoContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new FrigoDbInitializer());
        }

        public static FrigoContext Create()
        {
            return new FrigoContext();
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
                if (type.BaseType == null) continue;
                Type entityType = type.BaseType.GetGenericArguments().Single();

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
