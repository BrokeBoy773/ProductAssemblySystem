using Serilog;

namespace ProductAssemblySystem.API.Extensions
{
    public static class CustomHostBuilderExtensions
    {
        public static void UseCustomLogger(this IHostBuilder hostBuilder)
        {
            Serilog.ILogger logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithMachineName()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            hostBuilder.UseSerilog(logger);
        }
    }
}
