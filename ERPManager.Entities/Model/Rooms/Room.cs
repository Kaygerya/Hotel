using ERPManager.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Entities.Model.Rooms
{
    public class Room : Entity, IEntity
    {
        [BsonElement("cm")]
        public string CompanyId { get; set; }
        [BsonElement("nm")]
        public int Number { get; set; }
        [BsonElement("bc")]
        public int BedCount { get; set; }

    }
}
