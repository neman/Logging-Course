using Enjoying.Logging.Abstractions;
using Enjoying.Logging.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Enjoying.Logging.Desktop
{
    public class Application
    {
        public static LoggingSetup UseEnjoyingLogging(LoggingOptions loggingOptions)
        {
            //TODO:Create LoggingSetup Factory class
            return new LoggingSetup(new LoggerFactory(), new SerilogAdapter(), loggingOptions);
        }

        public static LoggingSetup UseEnjoyingLogging()
        {
            //TODO:Create LoggingSetup Factory class
            return new LoggingSetup(new LoggerFactory(), new SerilogAdapter(), Configure().GetSection("LoggingOptions").Get<LoggingOptions>());
        }

        public static IConfiguration Configure()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
