using System.Collections.Generic;

namespace PicnicAuth.Dto
{
    public class AuthUsersInCompany
    {
        public IEnumerable<AuthUserInCompany> AuthUsers { get; set; }
    }
}