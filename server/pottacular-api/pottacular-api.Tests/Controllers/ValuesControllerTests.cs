using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using pottacular_api.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace pottacular_api.Tests.Controllers
{
    [TestClass]
    public class ValueControllerTests
    {   
        [TestMethod]
        public void Get()
        {   
            //// Arrange
            ValuesController controller = new ValuesController();

            //// Act
            ActionResult<IEnumerable<string>> result = controller.Get();
            ActionResult<string> result2 = controller.Get(1);

            //// Assert
            Assert.IsNotNull(result);
            IEnumerable<string> expectedValue = new string[] { "value1", "value2" };
            Assert.IsTrue(System.Linq.Enumerable.SequenceEqual(result.Value, expectedValue));
            Assert.AreEqual(result2.Value, "value");
        }
    }
}
