using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using pottacular_api;

namespace pottacular_api.Models
{
    /// <summary>
    /// A MongoDb collection used to test API interactions as features like containers, orchestration, CI/CD, and scalability are added
    /// </summary>
    public class TestRequest
    {
        /// <summary>
        /// The id of the TestRequest
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TestRequestId { get; set; }
        /// <summary>
        /// The name of the TestRequest
        /// </summary>
        [BsonElement("TestRequestName")]
        public string TestRequestName { get; set; }
    }
}
