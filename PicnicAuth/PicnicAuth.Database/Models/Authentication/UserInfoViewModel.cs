namespace PicnicAuth.Database.Models.Authentication
{
    public class UserInfoViewModel
    {
        public string Username { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}