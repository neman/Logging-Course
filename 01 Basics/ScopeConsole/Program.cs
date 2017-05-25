using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;

namespace Scope
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory().AddConsole(includeScopes: true);               

            ILogger logger = loggerFactory.CreateLogger<Program>();

            using (logger.BeginScope("Code block calling Service")){
                
                logger.LogInformation("Getting item {ID}", 5);

                var service = new Service(loggerFactory.CreateLogger<Service>());

                service.DoStuff();

                logger.LogWarning("End of block calling Service");                
            }
        }
    }
}