namespace PicnicAuth.Database.Models.Authentication
{
    public class RegisterBindingModel
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
