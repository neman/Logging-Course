using Microsoft.Extensions.Logging;

namespace Scope
{
    class Service
    {
        private readonly ILogger<Service> _logger;

        public Service(ILogger<Service> logger)
        {
            _logger = logger;
        }

        public void DoStuff()
        {
            // Some logic before calling DB

            using (_logger.BeginScope($"{nameof(DoStuff)} has started DB execution"))
            {
                _logger.LogInformation("Init some data");
                //...
                _logger.LogInformation("Getting data from DB");
                //...
                _logger.LogInformation("Data from DB {}", 42);
            }
        }
    }
}
