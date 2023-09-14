using Serilog;

namespace Chat.API.Extensions
{
    public static class LoggerExtension
    {
        public static void ConfigureLogger(this WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .Enrich.WithProperty("Service", builder.Configuration["ServiceName"])
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog(logger);
        }
    }
}
