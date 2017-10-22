using System.Data.Entity;

namespace PicnicAuth.Database
{
    public class PicnicAuthDatabaseInitializer : DropCreateDatabaseIfModelChanges<PicnicAuthContext>
    {
        protected override void Seed(PicnicAuthContext context)
        {
        }
    }
}