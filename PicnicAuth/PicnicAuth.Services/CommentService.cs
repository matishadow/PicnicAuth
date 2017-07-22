using System.Collections.Generic;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Social;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    class CommentService : CrudService<Comment>, ICommentService, IRequestDependency
    {
        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<Comment> Get()
        {

            return base.Get();
        }
    }
}
