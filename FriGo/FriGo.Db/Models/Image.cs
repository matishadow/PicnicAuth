﻿using System;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Recipes;

namespace FriGo.Db.Models
{
    public class Image : OwnedEntity
    {
        public byte[] ImageBytes { get; set; }
    }
}