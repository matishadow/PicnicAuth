using System;
using FluentValidation;

namespace PicnicAuth.Interfaces.Validation
{
    public interface IAbstractDatabaseValidator : IValidator
    {
        bool EntityExists<TDatabaseEntity>(Guid entityId) where TDatabaseEntity : class;
    }
}