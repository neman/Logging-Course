using Microsoft.Extensions.Logging;
using Enjoying.Logging.Abstractions;
using System;

namespace Enjoying.Logging
{
    public class LoggingSetup
    {
        public ILoggerFactory LoggerFactory { get; }        

        public LoggingSetup(ILoggerFactory loggerFactory,
                            ILoggerAdapter loggerAdapter,
                            LoggingOptions options)
        {
            try
            {
                loggerAdapter.AddLog(loggerFactory, options);
                LoggerFactory = loggerFactory;
                ApplicationLogging.UseLoggerFactory(LoggerFactory);
            }
            catch (Exception) { } //silently continue
        }        
    }
}
