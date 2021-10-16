namespace CKE.Infra.Logging.Infrastructure
{
    using System;
    using CKE.Shared.Helpers;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class LoggingConfigurationBuilder
    {
        public static LoggerConfiguration ConfigureMinimum(this LoggerConfiguration configuration, bool development = false)
        {
            if (EnvironmentHelper.GetEnvironment() == "dev")
            {
                configuration.WriteToFileConfiguration();
            }
            if (EnvironmentHelper.GetEnvironment() == "acc")
            {
                configuration.WriteToFileConfiguration();
            }
            if (EnvironmentHelper.GetEnvironment() == "prd")
            {
                configuration.WriteToElasticSearch();
            }

            return configuration
                   .MinimumLevel.Is(development ? Serilog.Events.LogEventLevel.Debug : Serilog.Events.LogEventLevel.Information)
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
                                        .Add("host", "MachineName")
                                        .Add("environment", "environment")
                                        .Add("thread", "ThreadId")
                                        .Build();
            return configuration.WriteTo.Console(expressionTemplate);
        }

        private static LoggerConfiguration WriteToElasticSearch(this LoggerConfiguration configuration)
        {
            return configuration.WriteTo.Elasticsearch();
        }

        private static LoggerConfiguration WriteToFileConfiguration(this LoggerConfiguration configuration)
        {
            var expressionTemplate = new JsonTemplateBuilder()
                                        .Add("timestamp", "UtcDateTime(@t)")
                                        .Add("level",
                                            "coalesce({Debug: 'DEBUG', Information: 'INFO', Warning: 'WARN', Error: 'ERROR', 'Fatal': 'FATAL'}[@l], @l)")
                                        .Add("message", "@m")
                                        .Add("exception", "@x")
                                        .Add("host", "MachineName")
                                        .Add("environment", "environment")
                                        .Add("thread", "ThreadId")
                                        .Build();
            var outputPath = @".\Temp\log.txt";
            return configuration.WriteTo.File(expressionTemplate, outputPath);
        }
    }
}
