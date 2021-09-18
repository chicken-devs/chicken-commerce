namespace CKE.Infra.Logging.Application
{
    using CKE.Infra.Logging.Infrastructure;
    using Serilog;

    public static class StartupFactory
    {
        public static IStartLogger Create()
        {
            var logger = LoggingConfigurationBuilder
                            .ConfigureMinimum(new LoggerConfiguration())
                            .CreateBootstrapLogger();

            return new StartupLogger(logger);
        }
    }
}
