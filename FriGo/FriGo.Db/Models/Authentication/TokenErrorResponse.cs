using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriGo.Db.Models.Authentication
{
    public class TokenErrorResponse
    {
        // ReSharper disable once InconsistentNaming
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
