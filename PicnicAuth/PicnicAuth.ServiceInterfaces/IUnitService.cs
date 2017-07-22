using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models.Ingredients;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IUnitService
    {
        IEnumerable<Unit> Get();
        Unit Get(Guid id);
        void Add(Unit unit);
        void Edit(Unit unit);
        void Delete(Guid id);
    }
}