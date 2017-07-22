using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FriGo.Db.Models.Social;

namespace FriGo.Db.Models
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
