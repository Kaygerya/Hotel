﻿using ERPManager.Entities.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Models.Users
{
    public class RegisterModel: User
    {
        [DisplayName("Password Again")]
        public string PasswordAgain { get; set; }
    }
}
