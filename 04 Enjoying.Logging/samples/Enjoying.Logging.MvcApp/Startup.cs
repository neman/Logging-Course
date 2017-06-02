using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Enjoying.Logging.MvcApp.Models;

namespace Enjoying.Logging.Mvc
{
    public class Startup
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment env, ILogger<Startup> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            _logger.LogInformation($"{nameof(Startup)} ctor begins");

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            

            _logger.LogInformation($"{nameof(Startup)} ctor ends");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            var connection = @"Server=(localdb)\mssqllocaldb;Database=Enjoying;Trusted_Connection=True;";
            services.AddDbContext<BloggingContext>(options => options
                                .UseSqlServer(connection)
                                .UseLoggerFactory(_loggerFactory)
                                .EnableSensitiveDataLogging());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseEnjoyingLogging();
            app.UseMvc();
            //app.UseExceptionHandler()

            var logger = ApplicationLogging.CreateLogger<Startup>();
            logger.LogInformation("Message from Configure Method");
        }
    }
}
