using System.Data.Entity.ModelConfiguration;

namespace PicnicAuth.Models.ModelConfigurations
{
    public class AuthUserTypeConfiguration : EntityTypeConfiguration<AuthUser>
    {
        public AuthUserTypeConfiguration()
        {
            HasKey(authUser => authUser.Id);
        }
    }
}