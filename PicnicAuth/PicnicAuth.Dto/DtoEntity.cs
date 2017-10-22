using System;

namespace PicnicAuth.Dto
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
