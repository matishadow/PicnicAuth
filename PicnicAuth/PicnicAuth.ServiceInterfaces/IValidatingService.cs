using System.Net;
using FluentValidation;
using PicnicAuth.Database.Models;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IValidatingService
    {
        bool IsValid<T>(IValidator validator, T entity);
        ValidationError GenerateError<T>(IValidator validator, T entity);
        HttpStatusCode GetStatusCode();
    }
}