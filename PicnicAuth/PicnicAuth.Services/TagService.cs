using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class TagService : CrudService<Tag>, ITagService, IRequestDependency
    {
        public TagService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
