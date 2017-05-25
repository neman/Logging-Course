using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace LoggerFactoryInProgramMain
{
    public class Program
    {
        private static ILogger _logger;

        public static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory().AddConsole();
            _logger = loggerFactory.CreateLogger<Program>();
            _logger.LogInformation($"Hello from {nameof(Main)} before building host");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseLoggerFactory(loggerFactory)
                .Build();

            host.Run();
        }
    }
}
