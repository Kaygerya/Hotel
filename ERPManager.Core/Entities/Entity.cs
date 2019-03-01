using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ERPManager.Core.Entities
{
    public class Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cb")]
        public string CreatedBy { get; set; }
        [BsonElement("cd")]
        public DateTime CreatedDate { get; set; }
        [BsonElement("ub")]
        public string UpdatedBy { get; set; }
        [BsonElement("ud")]
        public DateTime UpdateDate { get; set; }
    }
}
