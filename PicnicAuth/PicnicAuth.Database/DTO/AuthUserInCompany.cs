namespace PicnicAuth.Database.DTO
{
    public class AuthUserInCompany : DtoEntity
    {
        public string ExternalId { get; set; }
        public string UserName { get; set; }
    }
}