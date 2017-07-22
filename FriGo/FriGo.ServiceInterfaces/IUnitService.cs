using System;
using System.Collections.Generic;
using FriGo.Db.Models.Ingredients;

namespace FriGo.ServiceInterfaces
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