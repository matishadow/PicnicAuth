using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PicnicAuth.Database.DAL;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Validation;

namespace PicnicAuth.Implementations.Validation
{
    public class AbstractDatabaseValidator<TValidatedEntity> : AbstractContinueValidator<TValidatedEntity>,
        IAbstractDatabaseValidator, IRequestDependency
    {
        private readonly IUnitOfWork unitOfWork;

        public AbstractDatabaseValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool EntityExists<TDatabaseEntity>(Guid entityId) where TDatabaseEntity : class
        {
            TDatabaseEntity entity = unitOfWork.Repository<TDatabaseEntity>().GetById(entityId);

            return entity != null;
        }

        public bool IsFieldUnique<TDatabaseEntity>(Expression<Func<TDatabaseEntity, bool>> databasePredicate) where TDatabaseEntity : class
        {
            IEnumerable<TDatabaseEntity> entities = unitOfWork.Repository<TDatabaseEntity>().Get(databasePredicate);

            return !entities.Any();
        }
    }
}