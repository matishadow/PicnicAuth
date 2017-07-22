using System.Net;

namespace PicnicAuth.Database.Models
{
    public class MessageError : Error
    {
        public MessageError(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public MessageError(HttpStatusCode statusCode, string message)
        {
            Code = (int) statusCode;
            Message = message;
        }

        public MessageError()
        {
        }

        public string Message { get; set; }
    }
}
