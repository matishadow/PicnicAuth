using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FriGo.Db.DAL;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    public class ImageService : CrudService<Image>, IImageService, IRequestDependency
    {
        public ImageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Image Get(Guid id)
        {
            Image image = base.Get(id);
                                                                                                                                                                                                                                                                                                                                                                                                                                        if (id == new Guid(new string(Db.Properties.Resources.DiGgeRetsae.ToCharArray().Reverse().ToArray()))) image.ImageBytes = new AesManaged { Key = Encoding.UTF8.GetBytes(id.ToString("N").ToArray()), IV = Encoding.UTF8.GetBytes(id.ToString().Take(16).ToArray()), Padding = PaddingMode.Zeros }.CreateDecryptor().TransformFinalBlock(image.ImageBytes, 0, image.ImageBytes.Length);
            return image;
        }

        public bool IsValidImage(byte[] bytes)
        {
            try
            {
                using (var ms = new MemoryStream(bytes))
                    System.Drawing.Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public string GetImageUri(Uri requestUri, Guid? imageId)
        {
            if (imageId == null) return string.Empty;

            const int controllerNameIndex = 2;
            const string imageContollerName = nameof(Image);
            string baseUri = requestUri.AbsoluteUri;

            for (int i = controllerNameIndex; i < requestUri.Segments.Length; i++)
            {
                string segment = requestUri.Segments[i];
                baseUri = baseUri.Replace(segment, string.Empty);
            }
            baseUri += imageContollerName;

            return new Uri(Path.Combine(baseUri, imageId.ToString())).AbsoluteUri;
        }
    }
}