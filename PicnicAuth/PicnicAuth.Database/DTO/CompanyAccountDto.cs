using System.Collections.Generic;
using PicnicAuth.Database.Models;

namespace PicnicAuth.Database.DTO
{
    public class CompanyAccountDto : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public IEnumerable<AuthUserInCompany> AuthUsers { get; set; }
    }
}