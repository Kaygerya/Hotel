using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Entities.Model.Users
{
    public class User: Entity,IEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
