using System;
using System.Collections.Generic;
using System.Linq;
using PicnicAuth.ServiceInterfaces.Dependencies;

namespace PicnicAuth.Database.DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork, IRequestDependency
    {
        private readonly PicnicAuthContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(PicnicAuthContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }        

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)))
            {
                return repositories[typeof(T)] as IGenericRepository<T>;
            }

            IGenericRepository<T> repository = new GenericRepository<T>(context);
            repositories.Add(typeof(T), repository);
            return repository;
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}