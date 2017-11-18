namespace PicnicAuth.Dto
{
    public class AuthUserInCompany : DtoEntity
    {
        public string ExternalId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}