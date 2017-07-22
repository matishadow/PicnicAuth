﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Attributes;
using FriGo.Db.ModelValidators;

namespace FriGo.Db.Models.Authentication
{
    [Validator(typeof(SetPasswordValidator))]
    public class SetPasswordBindingModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
