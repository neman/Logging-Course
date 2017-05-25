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
