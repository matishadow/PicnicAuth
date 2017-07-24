using System;
using FluentValidation;

namespace PicnicAuth.Database.ModelValidators.Interfaces
{
    public interface IAbstractDatabaseValidator : IValidator
    {
        bool EntityExists<TDatabaseEntity>(Guid entityId) where TDatabaseEntity : class;
    }
}