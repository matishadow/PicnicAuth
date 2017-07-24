using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;

namespace PicnicAuth.Database
{
    public class PicnicAuthDatabaseInitializer : DropCreateDatabaseIfModelChanges<PicnicAuthContext>
    {
        protected override void Seed(PicnicAuthContext context)
        {
        }
    }
}