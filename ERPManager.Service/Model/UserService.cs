using ERPManager.Entities.Model.Base;
using ERPManager.Entities.Model.Users;
using ERPManager.Service.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Service
{
    public class UserService : IUserService
    {
        public User GetUserById(string Id)
        {
            return new User();
        }
        public BaseModel LoginUser(string userName, string password)
        {
            return new BaseModel();
        }

    }
}
