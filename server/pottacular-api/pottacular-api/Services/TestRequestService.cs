using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using pottacular_api.Models;

namespace pottacular_api.Services
{
    public class TestRequestService
    {
        private readonly IMongoCollection<TestRequest> _testRequest;
        public TestRequestService (IPottacularDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _testRequest = database.GetCollection<TestRequest>(settings.TestRequestCollectionName);
        }

        public List<TestRequest> Get() =>
            _testRequest.Find(testRequest => true).ToList();

        public TestRequest Get(string id) =>
            _testRequest.Find<TestRequest>(testRequest => testRequest.TestRequestId == id).FirstOrDefault();

        public TestRequest Create(TestRequest testRequest)
        {
            _testRequest.InsertOne(testRequest);
            return testRequest;
        }

        public void Update(string id, TestRequest testRequestIn) =>
            _testRequest.ReplaceOne(testRequst => testRequst.TestRequestId == id, testRequestIn);
        
        public void Remove(TestRequest testRequestIn) =>
            _testRequest.DeleteOne(testRequest => testRequest.TestRequestId == testRequestIn.TestRequestId);
            
        public void Remove(string id) =>
            _testRequest.DeleteOne(testRequest => testRequest.TestRequestId == id);
    }
}
