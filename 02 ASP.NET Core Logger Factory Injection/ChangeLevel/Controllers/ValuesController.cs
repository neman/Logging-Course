using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChangeLevel.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly MyService _service;

        public ValuesController(ILogger<ValuesController> logger, MyService service)
        {
            _logger = logger;
            _service = service;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            _logger.LogInformation("I'm enjoying in get method");

            _logger.LogError("I'm not enjoying in get method");



            return new string[] { "value1", "value2", _service.MyValue };
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
