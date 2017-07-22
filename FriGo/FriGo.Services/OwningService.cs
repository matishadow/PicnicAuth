using System;
using System.Net;
using System.Security.Principal;
using FriGo.Db.Models;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;
using Microsoft.AspNet.Identity;

namespace FriGo.Services
{
    public class OwningService : IOwningService, IRequestDependency
    {
        private const HttpStatusCode ForbiddenStatusCode = HttpStatusCode.Forbidden;

        public bool IsOwnedByUser(Guid userId, OwnedEntity entity)
        {
            if (entity == null) return false;

            return entity.OwnerId == userId;
        }

        public bool IsOwnedByUser(IPrincipal user, OwnedEntity entity)
        {
            if (user?.Identity == null || entity == null) return false;

            return new Guid(user.Identity.GetUserId()) == entity.OwnerId;
        }

        public void AddOwnership(IPrincipal user, OwnedEntity entity)
        {
            if (user?.Identity == null || entity == null) return;
            
            entity.OwnerId = new Guid(user.Identity.GetUserId());
        }

        public MessageError GenerateForbiddenDeleteError()
        {
            return new MessageError(ForbiddenStatusCode, Db.Properties.Resources.GenericNotOwnerDeleteMessage);
        }

        public MessageError GenerateForbiddenPutError()
        {
            return new MessageError(ForbiddenStatusCode, Db.Properties.Resources.GenericNowOwnerPutMessage);
        }

        public MessageError GenerateForbiddenError(string message)
        {
            return new MessageError(ForbiddenStatusCode, message);
        }
    }
}