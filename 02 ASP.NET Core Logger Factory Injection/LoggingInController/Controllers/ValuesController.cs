using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace LoggingInController.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _loggerFromFactory;
        private readonly ILogger _logger;        

        public ValuesController(ILoggerFactory loggerFactory, ILogger<ValuesController> logger)
        {
            _loggerFromFactory = loggerFactory.CreateLogger<ValuesController>();
            _logger = logger;                        
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var _loggerFromServices = this.HttpContext.RequestServices.GetService<ILoggerFactory>().CreateLogger("Values");

            _loggerFromFactory.LogWarning(new EventId(42), "Logger from factory");
            _logger.LogWarning(new EventId(43), "Logger from DI");            
            _loggerFromServices.LogWarning(new EventId(44), "Logger from services");

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
