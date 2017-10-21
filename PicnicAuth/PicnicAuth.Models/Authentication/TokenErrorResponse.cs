// ReSharper disable once InconsistentNaming
// in order to be compatible with OAuth Clients
namespace PicnicAuth.Models.Authentication
{
    public class TokenErrorResponse
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
