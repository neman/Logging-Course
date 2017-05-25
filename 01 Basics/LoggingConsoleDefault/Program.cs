using Microsoft.Extensions.Logging;
using System;

namespace LoggingConsoleDefault
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();

            ILogger logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("Are you OK Computer");
            logger.LogError("System failed");
            logger.LogCritical("The world collapsed");
        }
    }
}