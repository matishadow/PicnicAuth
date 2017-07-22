using System;
using System.Collections.Generic;

namespace FriGo.ServiceInterfaces
{
    public interface ICrudService<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(Guid id);
        void Add(TEntity ingredient);
        void Edit(TEntity ingredient);
        void Delete(Guid id);
    }
}