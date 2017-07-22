﻿using System.Net;
using FluentValidation;
using FriGo.Db.Models;

namespace FriGo.ServiceInterfaces
{
    public interface IValidatingService
    {
        bool IsValid<T>(IValidator validator, T entity);
        ValidationError GenerateError<T>(IValidator validator, T entity);
        HttpStatusCode GetStatusCode();
    }
}