using System;

namespace PicnicAuth.Database.DTO
{
    public class DtoEntity
    {
        protected DtoEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
