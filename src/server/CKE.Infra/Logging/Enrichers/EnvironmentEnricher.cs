namespace CKE.Infra.Logging.Enrichers
{
    using System;
    using CKE.Shared.Helpers;
    using Serilog.Core;
    using Serilog.Events;

    public class EnvironmentEnricher : ILogEventEnricher
    {
        private LogEventProperty _property;

        public const string EnvironmentNamePropertyName = "environment";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            _ = logEvent ?? throw new ArgumentNullException(nameof(logEvent));
            _ = propertyFactory ?? throw new ArgumentNullException(nameof(propertyFactory));

            _property ??= GetProperty(propertyFactory);
            logEvent.AddPropertyIfAbsent(_property);
        }

        private LogEventProperty GetProperty(ILogEventPropertyFactory propertyFactory)
        {
            var environmentName = EnvironmentHelper.GetEnvironment();

            return propertyFactory.CreateProperty(EnvironmentNamePropertyName, environmentName);
        }
    }
}
