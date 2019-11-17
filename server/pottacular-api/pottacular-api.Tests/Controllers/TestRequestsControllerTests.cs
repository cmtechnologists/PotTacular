using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using pottacular_api.Controllers;
using pottacular_api.Models;
using pottacular_api.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace pottacular_api.Tests.Controllers
{
    [TestClass()]
    public class TestRequestsControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // Arrange
            PottacularDatabaseSettings settings = configureDbConnection();            
            TestRequestService _testRequestService = new TestRequestService(settings);
            TestRequestsController controller = new TestRequestsController(_testRequestService);
            // minimum satisfying response data to verify successful db api interaction
            List<TestRequest> expectedValue = new List<TestRequest>();
            expectedValue.Add(new TestRequest { TestRequestName = "test insert 1" });
            expectedValue.Add(new TestRequest { TestRequestName = "test insert 2" });
            
            // Act
            ActionResult<List<TestRequest>> response = controller.Get();
            ActionResult<TestRequest> resultData = controller.Get(response.Value[0].TestRequestId);
            
            // Assert
            // controller response
            Assert.IsNotNull(response);
            // expected test data presence
            Assert.AreEqual(expectedValue[0].TestRequestName, response.Value[0].TestRequestName);
            Assert.AreEqual(expectedValue[1].TestRequestName, response.Value[1].TestRequestName);
            Assert.AreEqual(resultData.Value.TestRequestName, "test insert 1");
        }
        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            PottacularDatabaseSettings settings = configureDbConnection();
            TestRequestService _testRequestService = new TestRequestService(settings);
            TestRequestsController controller = new TestRequestsController(_testRequestService);
            TestRequest createRequest = new TestRequest { TestRequestName = "test insert 3 by api" };

            // Act
            ActionResult<TestRequest> createResponse = controller.Create(createRequest).Result;
            CreatedAtRouteResult createRouteResult = createResponse.Result as CreatedAtRouteResult;

            // Assert
            Assert.AreEqual(createRouteResult.StatusCode, 201);
            Assert.AreEqual((createRouteResult.Value as TestRequest).TestRequestName, "test insert 3 by api");
        }
        [TestMethod()]
        public void ModifyTest()
        {
            // Arrange
            PottacularDatabaseSettings settings = configureDbConnection();
            TestRequestService _testRequestService = new TestRequestService(settings);
            TestRequestsController controller = new TestRequestsController(_testRequestService);

            // Act
            TestRequest recordToModify = controller.Get().Value.Where(tr => tr.TestRequestName == "test insert 3 by api").FirstOrDefault();
            recordToModify.TestRequestName = "test insert 3 by api (modified)";
            NoContentResult modifyResponse = controller.Update(recordToModify.TestRequestId, recordToModify) as NoContentResult;
            TestRequest modifiedRecord = controller.Get().Value.Where(tr => tr.TestRequestId == recordToModify.TestRequestId).FirstOrDefault();

            // Assert
            Assert.AreEqual(modifyResponse.StatusCode, 204);
            Assert.IsTrue(modifiedRecord.TestRequestName.Contains("(modified)"));
        }
        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            PottacularDatabaseSettings settings = configureDbConnection();
            TestRequestService _testRequestService = new TestRequestService(settings);
            TestRequestsController controller = new TestRequestsController(_testRequestService);
            // get record to delete
            List<TestRequest> resultData = controller.Get().Value;
            TestRequest recordToDelete = resultData.Where(tr => tr.TestRequestName == "test insert 3 by api (modified)").FirstOrDefault();

            // Act
            NoContentResult deleteResponse = controller.Delete(recordToDelete.TestRequestId) as NoContentResult;
            ActionResult<TestRequest> requestDeletedRecord = controller.Get(recordToDelete.TestRequestId);

            // Assert
            Assert.AreEqual(deleteResponse.StatusCode, 204);
            Assert.IsTrue(requestDeletedRecord.Value == null);
            Assert.IsTrue(requestDeletedRecord.Result is NotFoundResult);
        }
        public PottacularDatabaseSettings configureDbConnection()
        {
            // get file path to api project's appsettings.Development.json (4 levels up from test project build files)
            string projectParentDirectory = TraverseToParentN(System.IO.Directory.GetCurrentDirectory(), 4);

            string appSettingsPath = projectParentDirectory + @"\pottacular-api\appsettings.Development.json";
            JObject configObj = JObject.Parse(File.ReadAllText(appSettingsPath));
            JToken pottacularDbSettings = configObj["PottacularDatabaseSettings"];

            PottacularDatabaseSettings settings = new PottacularDatabaseSettings
            {
                TestRequestCollectionName = (string)pottacularDbSettings["TestRequestCollectionName"],
                ConnectionString = (string)pottacularDbSettings["ConnectionString"],
                DatabaseName = (string)pottacularDbSettings["DatabaseName"]
            };

            // connect to port 50340 instead of 27017 since connection is coming from Docker host during tests
            settings.ConnectionString = settings.ConnectionString.Replace("27017", "50340");
            settings.ConnectionString = settings.ConnectionString.Replace("pottacular-api_mongo_1", "localhost");
            return settings;
        }
        public string TraverseToParentN(string currentFilePath, int nthParent)
        {
            string result = currentFilePath;
            for (int i = 0; i < nthParent; i++)
            {
                result = Directory.GetParent(result).FullName;
            }
            return result;
        }
    }
}