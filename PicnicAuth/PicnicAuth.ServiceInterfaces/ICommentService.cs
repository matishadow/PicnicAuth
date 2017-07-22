using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models.Social;

namespace PicnicAuth.ServiceInterfaces
{
    public interface ICommentService
    {
        IEnumerable<Comment> Get();
        Comment Get(Guid id);
        void Add(Comment comment);
        void Edit(Comment comment);
        void Delete(Guid id);
    }
}
