namespace CKE.Infra.Logging.Infrastructure
{
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class HostBuilderExtension
    {
        public static IHostBuilder ConfigureLogging(this IHostBuilder builder)
        {
            return builder.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .Enrich.WithThreadId()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironment()
                    .Configure(context, services);
            });
        }
    }
}
