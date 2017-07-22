using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.ServiceInterfaces
{
    public interface ITagService
    {
        IEnumerable<Tag> Get();
        Tag Get(Guid id);
        void Add(Tag ingredient);
        void Edit(Tag ingredient);
        void Delete(Guid id);
    }
}
