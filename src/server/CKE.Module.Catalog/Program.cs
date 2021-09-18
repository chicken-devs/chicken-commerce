using System;
using CKE.Infra.Logging.Application;
using CKE.Infra.Logging.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CKE.Module.Catalog
{
    public class Program
    {
        private static readonly IStartLogger Logger = StartupFactory.Create();
        public static void Main(string[] args)
        {
            try
            {
                Logger.LogInformation("Application is starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Application stopped!!!");
            }
            finally
            {
                Logger.Close();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
