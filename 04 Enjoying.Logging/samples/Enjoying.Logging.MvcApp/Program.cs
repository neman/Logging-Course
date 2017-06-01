using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Enjoying.Logging.Adapters;
using Microsoft.Extensions.Configuration;
using Enjoying.Logging.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Enjoying.Logging.Abstractions;

namespace Enjoying.Logging.Mvc
{
    public class Program
    {
       public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseEnjoyingLogging<SerilogAdapter>()                
                .Build();            

            host.Run();
        }
    }
}
