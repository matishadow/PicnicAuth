using System;
using System.Security.Principal;
using PicnicAuth.Database.Models;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IOwningService
    {
        bool IsOwnedByUser(Guid userId, OwnedEntity entity);
        bool IsOwnedByUser(IPrincipal user, OwnedEntity entity);
        void AddOwnership(IPrincipal user, OwnedEntity entity);
        MessageError GenerateForbiddenDeleteError();
        MessageError GenerateForbiddenPutError();
        MessageError GenerateForbiddenError(string message);
    }
}