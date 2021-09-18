namespace CKE.Infra.Logging.Infrastructure
{
    using System;
    using CKE.Infra.Logging.Enrichers;
    using Serilog;
    using Serilog.Configuration;

    public static class EnricherConfigurationExtension
    {
        public static LoggerConfiguration WithEnvironment(this LoggerEnrichmentConfiguration loggerEnrichment)
        {
            _ = loggerEnrichment ?? throw new ArgumentNullException(nameof(loggerEnrichment));

            return loggerEnrichment.With(new EnvironmentEnricher());
        }
    }
}
