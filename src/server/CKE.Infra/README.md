# Document for CKE.Infra.Logging

The instruction below shows how to configure the Logging.

## The section is the instruction on how to use the library in your project.
Please do following steps:

## Register in `Program.cs`

Please see the code below example for applying the extension to the system.

### Creating and using the StartupLogger
To bootstrap the log when the application starts, use the code below
```
public class Program
{
    public static void Main(string[] args)
    {   
        var logger = StartupLoggerFactory.Create();
        try
        {
            logger.LogInformation("Application is starting.");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            logger.Fatal(ex, "The application failed to start.");
        }
        finally
        {
            logger.Close();
        }
    }
}
```

## Apply the Serilog
We have to call `ConfigureLogging()` to use the Serilog
```
public class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
```

## How to use Logging

The fisrt add reference to CKE.Infra project
```
[ApiController]
[Route("[controller]")]
public class CatalogsController : ControllerBase
{
    private readonly ILogger<CatalogsController> _logger;

    public CatalogsController(ILogger<CatalogsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<bool> Get()
    {
        _logger.LogInformation("Message here");
        return true;
    }
}
```
- LogEventLevel.Debug is current host environment name
- LogEventLevel.Information for the others

# Changelog
## 2021.1.0
* Initial version

# Author Huyen Nguyen
