#### 01 Example
First example
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
5. Add new .NET Core Console App SpecificConsoleLoggerProvider
6. Copy Program.cs class from previous example
7. Add the following code instead the one from step 2.
```csharp
loggerFactory.AddProvider(
  new ConsoleLoggerProvider(
    (text, logLevel) => logLevel >= LogLevel.Verbose , true));
```
8. Show on github how LogLevel.Verbose has changed to LogLevel.Trace
https://github.com/aspnet/Logging/blob/9506ccc3f3491488fe88010ef8b9eb64594abf95/src/Microsoft.Extensions.Logging.Abstractions/LogLevel.cs
https://github.com/aspnet/Logging/commit/7a05d121d6c120a15d5a2178e3e45d7c400d22c0
9. Change to LogLevel.Trace and add the following code
```csharp
logger.LogTrace("Tracing...");
```
10. Change code from
```csharp
loggerFactory.AddProvider(
                new ConsoleLoggerProvider((text, logLevel) => logLevel >= LogLevel.Trace, true));
loggerFactory.AddProvider(
                new ConsoleLoggerProvider((text, logLevel) => logLevel >= LogLevel.Error, true));
```