using ERPManager.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Entities.Model.Users
{
    [BsonIgnoreExtraElements]
    public class UserAuthorizations :Entity , IEntity
    {
        public UserAuthorizations()
        {
            Authorizations = new List<UserAuthorization>();
        }
        List<UserAuthorization> Authorizations { get; set; }
    }


    [BsonIgnoreExtraElements]
    public class UserAuthorization 
    {
        [BsonElement("cm")]
        public string CompanyId { get; set; }
        [BsonElement("at")]
        public AuthorizationType AuthorizationType { get; set; }
    }

    public enum AuthorizationType
    {
        Owner = 1,
        Admin =2,
        Receptionist =3,
        Accounting =4

    }

}
