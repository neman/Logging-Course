using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;

namespace SpecificConsoleLoggerProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory();

            loggerFactory.AddProvider(
                new ConsoleLoggerProvider((text, logLevel) => logLevel >= LogLevel.Trace, true));

            ILogger logger = loggerFactory.CreateLogger<Program>();

            logger.LogTrace("Tracing...");
            logger.LogInformation("Are you OK Computer");
            logger.LogError("System failed");
            logger.LogCritical("The world collapsed");
        }
    }
}