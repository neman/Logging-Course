using Microsoft.AspNetCore.Builder;

namespace Enjoying.Logging.Mvc
{
    public static class LoggingBuilderExtensions
    {
        public static void UseEnjoyingLogging(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetService(typeof(LoggingSetup));
        }
    }
}
