using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SerilogExample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            _logger.LogTrace(new EventId(), new Exception("Demo"), "Trace message");
            _logger.LogDebug("Debug message");
            _logger.LogInformation("Information message");
            _logger.LogWarning("Warning message");
            _logger.LogError("Error message");
            _logger.LogCritical(new EventId(1), new NullReferenceException("Null ex"), "Critical message");

            try
            {
                var zero = 0;
                var x = zero/ (zero);
            }
            catch (DivideByZeroException ex)
            {
                _logger.LogError(new EventId(), ex, $"{nameof(Get)} action failed");
            }

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
