namespace CKE.Infra.Logging.Infrastructure
{
    using System;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class LoggingConfigurationBuilder
    {
        public static LoggerConfiguration ConfigureMinimum(this LoggerConfiguration configuration, bool development = false)
        {
            return configuration
                   .MinimumLevel.Is(development ? Serilog.Events.LogEventLevel.Debug : Serilog.Events.LogEventLevel.Information)
                   .ConsoleJsonConfiguration()
                   .Enrich.FromLogContext();
        }

        public static LoggerConfiguration Configure(this LoggerConfiguration configuration, HostBuilderContext context, IServiceProvider services)
        {
            return configuration.ConfigureMinimum(context.HostingEnvironment.IsDevelopment())
                                .ReadFrom.Configuration(context.Configuration)
                                .ReadFrom.Services(services);
        }

        public static LoggerConfiguration ConsoleJsonConfiguration(this LoggerConfiguration configuration)
        {
            var expressionTemplate = new JsonTemplateBuilder()
                                        .Add("timestamp", "UtcDateTime(@t)")
                                        .Add("level",
                                            "coalesce({Debug: 'DEBUG', Information: 'INFO', Warning: 'WARN', Error: 'ERROR', 'Fatal': 'FATAL'}[@l], @l)")
                                        .Add("message", "@m")
                                        .Add("exception", "@x")
                                        .Add("applicationName", "applicationName")
                                        .Add("applicationVersion", "applicationVersion")
                                        .Add("componentName", "componentName")
                                        .Add("host", "MachineName")
                                        .Add("environment", "environment")
                                        .Add("thread", "ThreadId")
                                        .Build();

            return configuration.WriteTo.Console(expressionTemplate);
        }
    }
}
