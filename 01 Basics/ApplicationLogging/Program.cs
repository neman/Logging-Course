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