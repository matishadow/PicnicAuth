using System;
using FluentValidation;

namespace PicnicAuth.Validation.Interfaces
{
    public interface IAbstractDatabaseValidator : IValidator
    {
        bool EntityExists<TDatabaseEntity>(Guid entityId) where TDatabaseEntity : class;
    }
}