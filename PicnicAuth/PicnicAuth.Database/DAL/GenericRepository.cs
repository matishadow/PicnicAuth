using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PicnicAuth.Database.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly IDbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> Get(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy?.Invoke(query).ToList() ?? query.ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Edit(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
            context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}