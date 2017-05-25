## 01 Example
#### LoggingConsoleDefault
1. Create new .NET Core Console App
2. Add following code
```csharp
var loggerFactory = new LoggerFactory()
                        .AddConsole()
                        .AddDebug();
```
3. Add packages Microsoft.Extensions.Logging, Microsoft.Extensions.Logging.Console, Microsoft.Extensions.Logging.Debug
4. Add code following code
```csharp
ILogger logger = loggerFactory.CreateLogger<Program>();
            
logger.LogInformation("Are you OK Computer");
logger.LogError("System failed");
logger.LogCritical("The world collapsed");
```
#### SpecificConsoleLoggerProvider
1. Add new .NET Core Console App SpecificConsoleLoggerProvider
2. Copy Program.cs class from previous example
3. Add the following code instead the one from `LoggingConsoleDefault step 2`.
```csharp
loggerFactory.AddProvider(new ConsoleLoggerProvider((text, logLevel) => logLevel >= LogLevel.Verbose , true));
```
4. Show on github how LogLevel.Verbose has changed to LogLevel.Trace
https://github.com/aspnet/Logging/blob/9506ccc3f3491488fe88010ef8b9eb64594abf95/src/Microsoft.Extensions.Logging.Abstractions/LogLevel.cs
https://github.com/aspnet/Logging/commit/7a05d121d6c120a15d5a2178e3e45d7c400d22c0
5. Change to LogLevel.Trace and add the following code
```csharp
logger.LogTrace("Tracing...");
```
6. Change code from
```csharp
loggerFactory.AddProvider(
                new ConsoleLoggerProvider((text, logLevel) => logLevel >= LogLevel.Trace, true));
loggerFactory.AddProvider(
                new ConsoleLoggerProvider((text, logLevel) => logLevel >= LogLevel.Error, true));
```
#### ApplicationLogging
1. Create new .NET Core Console App ApplicationLogging
2. Add file `LoggingHelper.cs` with following content
```csharp
using Microsoft.Extensions.Logging;

namespace ApplicationLogging
{
    public static class LoggingHelper
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
        public static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);
    }
}
```
3. `Program.cs` file should look like 
```csharp
using Microsoft.Extensions.Logging;
using System;

namespace ApplicationLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggingHelper.LoggerFactory.AddConsole();

            LoggingHelper.CreateLogger<Program>().LogInformation("Before Hello World");

            Console.WriteLine($"{Environment.NewLine} Hello World! {Environment.NewLine}");

            LoggingHelper.CreateLogger("Program").LogInformation(new EventId(66), "After Hello World");
        }
    }
}
```
#### ScopeConsole
1. Create new .NET Core Console App ScopeConsole
2. Add new file `Service.cs` with the following code
```csharp
using Microsoft.Extensions.Logging;

namespace Scope
{
    class Service
    {
        private readonly ILogger<Service> _logger;

        public Service(ILogger<Service> logger)
        {
            _logger = logger;
        }

        public void DoStuff()
        {
            // Some logic before calling DB

            using (_logger.BeginScope($"{nameof(DoStuff)} has started DB execution"))
            {
                _logger.LogInformation("Init some data");
                //...
                _logger.LogInformation("Getting data from DB");
                //...
                _logger.LogInformation("Data from DB {}", 42);
            }
        }
    }
}
```
3. Change Main method in `Program.cs`. 
> Note: includeScopes must be true
```csharp
static void Main(string[] args)
{
    var loggerFactory = new LoggerFactory().AddConsole(includeScopes: true);               
    
    ILogger logger = loggerFactory.CreateLogger<Program>();

    using (logger.BeginScope("Code block calling Service"))
    {
                
    logger.LogInformation("Getting item {ID}", 5);

    var service = new Service(loggerFactory.CreateLogger<Service>());

    service.DoStuff();

    logger.LogWarning("End of block calling Service");                
    }
}
```
## 02 Example
#### LoggerFactoryInjection
1. Create ASP.NET Core Web Api project
2. Set to run as ConsoleApp (Do not use IIS Express)
3. Set breakpoint at Startup Configure method and show that LoggerFactory is instantiated.
```csharp
loggerFactory.AddConsole(Configuration.GetSection("Logging"));
```
4. Where is the magic? What calls Configure() and where the ILoggerFactory instance actually came from and live at.
Go to Program.cs -> Main() -> Build() -> F12 (Microsoft.AspNetCore.Hosting.Abstractions)
5. Go to Github links
    https://github.com/aspnet/Hosting/blob/7ac6842d1864fd97c417abd016440893c3384b12/src/Microsoft.AspNetCore.Hosting/WebHostBuilder.cs#L213
    https://github.com/aspnet/Hosting/blob/7ac6842d1864fd97c417abd016440893c3384b12/src/Microsoft.AspNetCore.Hosting/WebHostBuilder.cs#L237
    https://github.com/aspnet/Hosting/blob/7ac6842d1864fd97c417abd016440893c3384b12/src/Microsoft.AspNetCore.Hosting/WebHostBuilder.cs#L255
    https://github.com/aspnet/Hosting/blob/7ac6842d1864fd97c417abd016440893c3384b12/src/Microsoft.AspNetCore.Hosting/WebHostBuilder.cs#L316    
```csharp
 public IWebHost Build()
 ...
 var hostingServices = BuildCommonServices(out var hostingStartupErrors);
 ...
 private IServiceCollection BuildCommonServices(out AggregateException hostingStartupErrors)
 ...
  // The configured ILoggerFactory is added as a singleton here. AddLogging below will not add an additional one.
var loggerFactory = _createLoggerFactoryDelegate?.Invoke(_context) ?? new LoggerFactory();

services.AddSingleton(loggerFactory);
_context.LoggerFactory = loggerFactory;

foreach (var configureLogging in _configureLoggingDelegates)
{
    configureLogging(_context, loggerFactory);
}

//This is required to add ILogger of T.
services.AddLogging();
```
6. Show AddLogging extension source code from https://github.com/aspnet/Logging/blob/dev/src/Microsoft.Extensions.Logging/LoggingServiceCollectionExtensions.cs#L20
```csharp
public static class LoggingServiceCollectionExtensions
{
        /// <summary>
        /// Adds logging services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));

            return services;
        }
}
```
#### LoggerFactoryInStartupCtor
1. Add new ASP.NET Core Web Api project LoggerFactoryInStartupCtor
2. Add private field _logger and change Startp ctor
```csharp
private readonly ILogger _logger;

public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    loggerFactory.AddConsole();
    _logger = loggerFactory.CreateLogger<Startup>();
    _logger.LogInformation($"Welcome from {nameof(Startup)}");

    var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
        Configuration = builder.Build();
}
```
3. Show that there are now double entries of log (Because of the same provider added 2 times)
![Double Log Entry](https://raw.githubusercontent.com/neman/Logging-Course/master/Images/DoubleLogEntry.png)
#### LoggerFactoryInProgramMain
1. Add new ASP.NET Core Web Api project LoggerFactoryInProgramMain
2. Show IWebHostBuilder method `public IWebHostBuilder UseLoggerFactory(ILoggerFactory loggerFactory);`
3. Add the following code to Program.cs
```csharp
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
```
4. Remove `loggerFactory.AddConsole(Configuration.GetSection("Logging"));` from Configure method in Startup class
5. Show the working demo
6. Bonus: Latest interesting issue example on github https://github.com/aspnet/Announcements/issues/241
#### LoggingInController
1. Create new new ASP.NET Core Web Api project LoggingInController
2. Add `using Microsoft.Extensions.DependencyInjection;` in `ValuesController`
3. Add following fields
```csharp
private readonly ILogger _loggerFromFactory;
private readonly ILogger _logger;   
```
4. Change constructor to
```csharp
public ValuesController(ILoggerFactory loggerFactory, ILogger<ValuesController> logger)
{
    _loggerFromFactory = loggerFactory.CreateLogger<ValuesController>();
    _logger = logger;                        
}
```
5. Change Get Action to
```csharp
[HttpGet]
public IEnumerable<string> Get()
{
    var _loggerFromServices = this.HttpContext.RequestServices.GetService<ILoggerFactory>()
                                                              .CreateLogger("Values");
    
    _loggerFromFactory.LogWarning(new EventId(42), "Logger from factory");
    _logger.LogWarning(new EventId(43), "Logger from DI");            
    _loggerFromServices.LogWarning(new EventId(44), "Logger from services");

    return new string[] { "value1", "value2" };
}
```
![LoggingInController](https://raw.githubusercontent.com/neman/Logging-Course/master/Images/LoggingInController.png)
## 03 Example
#### Rolling File in one line
1. Create new empty sln RollingFile
2. Create new .NET Core WebApi RollingFile
3. Add package reference to `Serilog.Extensions.Logging.File`
4. Add `loggerFactory.AddFile("Logs/Nemke-{Date}.txt");` to Startup Configure method
5. Change code to
```csharp
loggerFactory.AddFile("Logs/log-{Date}.txt", isJson:false, minimumLevel:LogLevel.Trace);

var logger = loggerFactory.CreateLogger<Startup>();
logger.LogTrace(new EventId(), new Exception("Demo"), "Trace message");
logger.LogDebug("Debug message");
logger.LogInformation("Information message");
logger.LogWarning("Warning message");
logger.LogError("Error message");
logger.LogCritical(new EventId(), new FormatException(),"Critical message");            
```
6. Show Log txt file (with regular and json entries)
#### NLog Lab
1. Do the lab by the link examples at 
    - https://github.com/NLog/NLog.Web/wiki/Getting-started-with-ASP.NET-Core-(project.json)
    - https://github.com/NLog/NLog.Web/wiki/Getting-started-with-ASP.NET-Core-(csproj---vs2017)
and try to integrate MEL with NLog.
#### Serilog
Before Serilog example, show slides about structure logging. 
1. Show github serilog sink links https://github.com/serilog/serilog/wiki/Provided-Sinks
2. Create new .NET Core WebApi SerilogExample Project
3. Add package reference to `Serilog.Extensions.Logging`, `Serilog`, `Serilog.Sinks.Literate`, `Serilog.Sinks.Seq` 
4. Add following code in Startup ctor
```csharp
 Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.LiterateConsole()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();
```
5. Add following code in Configure method `loggerFactory.AddSerilog();`
6. Change ValuesController 
