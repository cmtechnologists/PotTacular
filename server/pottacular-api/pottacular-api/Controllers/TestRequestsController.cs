using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pottacular_api.Models;
using pottacular_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace pottacular_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRequestsController : ControllerBase
    {
        private readonly TestRequestService _testRequestService;

        public TestRequestsController(TestRequestService testRequestService)
        {
            _testRequestService = testRequestService;
        }
        /// <summary>
        /// Get a list of all TestRequests in the collection
        /// </summary>
        /// <returns>An ActionResult of type List of type TestRequest</returns>
        [HttpGet]
        public ActionResult<List<TestRequest>> Get() =>
                _testRequestService.Get();
        /// <summary>
        /// Get a TestRequest by id
        /// </summary>
        /// <param name="id">The id (24 digit hex string) of the TestRequest you want to retrieve</param>
        /// <returns>A JSON doc of the requested TestRequest or 404</returns>
        [HttpGet("{id:length(24)}", Name = "GetTestRequest")]
        public ActionResult<TestRequest> Get(string id)
        {
            var testRequest = _testRequestService.Get(id);
            if (testRequest == null)
            {
                return NotFound();
            }
                return testRequest;
        }
        /// <summary>
        /// Create a new TestRequest
        /// </summary>
        /// <param name="testRequest">The name of the TestRequest</param>
        /// <returns>201</returns>
        [HttpPost]
        public ActionResult<TestRequest> Create(TestRequest testRequest)
        {
            _testRequestService.Create(testRequest);
            return CreatedAtRoute("GetTestRequest", new { id = testRequest.TestRequestId.ToString() }, testRequest);
        }
        /// <summary>
        /// Update a TestRequest by id
        /// </summary>
        /// <param name="id">The id (24 digit hex string) of the TestRequest you want to update</param>
        /// <param name="testRequestIn">The values to update the TestRequest with</param>
        /// <returns>204 or 404</returns>
        /// <remarks>
        /// Sample request updating the requested document's TestRequestName value \
        /// PUT /api/TestRequests \
        /// [ \
        ///     { \
        ///         "testrequestid": "5dd33e6578adb2721066a780",
        ///         "testrequestname": "my new request name"
        ///     } \
        /// ]
        /// </remarks>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, TestRequest testRequestIn)
        {
            var testRequest = _testRequestService.Get(id);
            if (testRequest == null)
            {
                return NotFound();
            }
                _testRequestService.Update(testRequest.TestRequestId, testRequestIn);
            return NoContent();
        }
        /// <summary>
        /// Delete a TestRequest by id
        /// </summary>
        /// <param name="id">The id (24 digit hex string) of the TestRequest you want to delete</param>
        /// <returns>204 or 404</returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var testReequest = _testRequestService.Get(id);
            if (testReequest == null)
            {
                return NotFound();
            }
                _testRequestService.Remove(testReequest.TestRequestId);
            return NoContent();
        }
    }

}
