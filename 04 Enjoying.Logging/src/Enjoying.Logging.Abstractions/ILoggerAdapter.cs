using Microsoft.Extensions.Logging;

namespace Enjoying.Logging.Abstractions
{
    public interface ILoggerAdapter
    {
        void AddLog(ILoggerFactory loggerFactory, LoggingOptions loggingOptions);
    }
}