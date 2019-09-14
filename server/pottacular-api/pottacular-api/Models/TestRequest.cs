using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using pottacular_api;

namespace pottacular_api.Models
{
    public class TestRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TestRequestId { get; set; }

        [BsonElement("TestRequestName")]
        public string TestRequestName { get; set; }
    }
}
