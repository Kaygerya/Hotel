using ERPManager.Entities.Model.Base;
using ERPManager.Entities.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Models.Users
{
    public class LoginModel  : BaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
