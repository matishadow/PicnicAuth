using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IImageService 
    {
        IEnumerable<Image> Get();
        Image Get(Guid id);
        void Add(Image image);
        void Edit(Image image);
        void Delete(Guid id);
        bool IsValidImage(byte[] bytes);
        string GetImageUri(Uri requestUri, Guid? imageId);
    }
}