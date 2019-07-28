using ERPManager.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Entities.Model.Company
{
    [BsonIgnoreExtraElements]
    public class Company : Entity,IEntity
    {
        [BsonElement("nm")]
        public string Name { get; set; }

    }
}
