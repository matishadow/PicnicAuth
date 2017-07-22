using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class UnitService : CrudService<Unit>, IUnitService, IRequestDependency
    {
        public UnitService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}