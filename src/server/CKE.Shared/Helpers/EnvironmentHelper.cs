namespace CKE.Shared.Helpers
{
    using System;

    public static class EnvironmentHelper
    {
        public static string GetEnvironment()
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

        private static string GetEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        }
    }
}
