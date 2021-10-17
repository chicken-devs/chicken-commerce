using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CKE.Module.Catalog.Controllers
{
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
            _logger.LogInformation("HUYEN TEST LOGGING");
            _logger.LogError("HUYEN TEST LogError");
            _logger.LogDebug("HUYEN TEST LogDebug");
            _logger.LogTrace("HUYEN TEST LogTrace");

            return true;
        }
    }
}
