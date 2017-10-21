using System.Collections.Generic;

namespace PicnicAuth.Dto
{
    public class CompanyAccountDto : DtoEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public IEnumerable<AuthUserInCompany> AuthUsers { get; set; }
    }
}