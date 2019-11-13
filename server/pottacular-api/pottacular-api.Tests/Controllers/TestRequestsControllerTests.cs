﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using pottacular_api.Controllers;
using pottacular_api.Models;
using pottacular_api.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace pottacular_api.Controllers.Tests
{
    [TestClass()]
    public class TestRequestsControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // Arrange
            // get file path to api project's appsettings.Development.json (4 levels up from test project build files)
            string projectParentDirectory = TraverseToParentN(System.IO.Directory.GetCurrentDirectory(), 4);
                
            string appSettingsPath = projectParentDirectory + @"\pottacular-api\appsettings.Development.json";
            JObject configObj = JObject.Parse(File.ReadAllText(appSettingsPath));
            JToken pottacularDbSettings = configObj["PottacularDatabaseSettings"];
            
            PottacularDatabaseSettings settings = new PottacularDatabaseSettings
            {
                TestRequestCollectionName = (string) pottacularDbSettings["TestRequestCollectionName"],
                ConnectionString = (string) pottacularDbSettings["ConnectionString"],
                DatabaseName = (string) pottacularDbSettings["DatabaseName"]
            };

            // connect to port 50340 instead of 27017 since connection is coming from Docker host during tests
            settings.ConnectionString = settings.ConnectionString.Replace("27017", "50340");
            settings.ConnectionString = settings.ConnectionString.Replace("pottacular-api_mongo_1", "localhost");
            TestRequestService _testRequestService = new TestRequestService(settings);
            TestRequestsController controller = new TestRequestsController(_testRequestService);
            
            // Act
            ActionResult<List<TestRequest>> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
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