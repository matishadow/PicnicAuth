using System.Collections.Generic;
using PicnicAuth.Database.Models.Authentication;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IUserService
    {
        User Get(string id);

        IEnumerable<User> Get();

        void Edit(User user);
    }
}
