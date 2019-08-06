using ERPManager.Entities.Model.Base;
using ERPManager.Entities.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Service.Abstract
{
    public interface IUserService
    {
        User GetUserById(string Id);
        BaseModel LoginUser(string userName, string password);
    }
}
