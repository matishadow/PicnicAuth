using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class UserService : CrudService<User>, IUserService, IRequestDependency
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User Get(string id)
        {
            return UnitOfWork.Repository<User>().GetById(id);
        }
    }
}
