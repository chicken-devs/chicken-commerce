namespace CKE.Infra.Logging.Enrichers
{
    using System;
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
            var environmentName = GetEnvironment();

            return propertyFactory.CreateProperty(EnvironmentNamePropertyName, environmentName);
        }

        private string GetEnvironment()
        {
            var raw = GetEnvironmentVariable();
            if (string.IsNullOrEmpty(raw))
            {
                return string.Empty;
            }
            var result = default(Shared.Enums.Environment);
            switch (raw.ToLower())
            {
                case "production":
                case "prod":
                case "prd":
                    result = Shared.Enums.Environment.PRD;
                    break;
                case "acceptance":
                case "acc":
                    result = Shared.Enums.Environment.ACC;
                    break;
                case "user-acceptance-testing":
                case "useracceptancetesting":
                case "user-acceptance":
                case "useracceptance":
                case "uat":
                    result = Shared.Enums.Environment.UAT;
                    break;

                case "system-acceptance-testing":
                case "systemacceptancetesting":
                case "system-acceptance":
                case "systemacceptance":
                case "system-testing":
                case "systemtesting":
                    result = Shared.Enums.Environment.ST;
                    break;

                case "continuous-integration-continuous-delivery":
                case "continuousintegrationcontinuousdelivery":
                case "cicd":
                    result = Shared.Enums.Environment.CICD;
                    break;

                case "continuous-integration":
                case "continuousintegration":
                case "ci":
                    result = Shared.Enums.Environment.CICD;
                    break;
                case "continuous-delivery":
                case "continuousdelivery":
                case "cd":
                    result = Shared.Enums.Environment.CICD;
                    break;

                case "testing":
                case "test":
                case "tst":
                    result = Shared.Enums.Environment.TST;
                    break;

                case "development":
                case "develop":
                case "dev":
                    result = Shared.Enums.Environment.DEV;
                    break;
            }

            return result.ToString().ToLower() ?? string.Empty;
        }

        private string GetEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        }
    }
}
