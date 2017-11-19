using System;

namespace PicnicAuth.Dto
{
    public class DtoEntity
    {
        public DtoEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
