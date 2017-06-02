﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using Enjoying.Logging.Abstractions;
using Console = Colorful.Console;
using Microsoft.Extensions.Options;
using Enjoying.Logging.Extensions.DependencyInjection;
using Enjoying.Logging.Adapters;

namespace Enjoying.Logging.ConsoleApp
{
    class Program
    {
        public IConfigurationRoot Configuration { get; }
        private static GUI _gui;
        static void Main(string[] args)
        {
            //Setup Configuration
            var configuration = Configure();

            var useDI = true;
            if (useDI) { _gui = GetGui(configuration); }
            else
            {
                var loggingSetupWithoutDI = Enjoying.Logging.Desktop.Application.UseEnjoyingLogging(configuration.GetSection("LoggingOptions").Get<LoggingOptions>());
                _gui = new GUI(new LoggingExamples(loggingSetupWithoutDI.LoggerFactory.CreateLogger<LoggingExamples>()));
            }

            _gui.UserInterface();

            var logger = ApplicationLogging.CreateLogger<Program>();
            logger.LogInformation($"End of {nameof(Main)} method");

        }

        private static GUI GetGui(IConfiguration configuration)
        {
            //Setup DI
            var services = new ServiceCollection();
            //Register services
            services.AddEnjoyingLogging<SerilogAdapter>(configuration);
            services.AddTransient<LoggingExamples>();
            //Build service container
            var serviceContainer = services.BuildServiceProvider();
            //Add(Resolve) Logging setup instance to Service Container
            serviceContainer.GetRequiredService<LoggingSetup>();

            return new GUI(serviceContainer.GetRequiredService<LoggingExamples>());
        }

        public static IConfiguration Configure()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            return builder.Build();
        }
    }
}