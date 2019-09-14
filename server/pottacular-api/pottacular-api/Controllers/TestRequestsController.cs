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

        [HttpGet]
        public ActionResult<List<TestRequest>> Get() =>
                _testRequestService.Get();

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

        [HttpPost]
        public ActionResult<TestRequest> Create(TestRequest testRequest)
        {
            _testRequestService.Create(testRequest);
            return CreatedAtRoute("GetTestRequest", new { id = testRequest.TestRequestId.ToString() }, testRequest);
        }

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
