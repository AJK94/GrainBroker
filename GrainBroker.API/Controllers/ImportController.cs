using GrainBroker.Domain;
using GrainBroker.Domain.Models;
using GrainBroker.Services;
using GrainBroker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrainBroker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly ILogger<ImportController> _logger;
        private readonly IImportService _importService;

        public ImportController(ILogger<ImportController> logger, IImportService importService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _importService = importService ?? throw new ArgumentNullException(nameof(importService));
        }

        [HttpGet]
        public async Task<IActionResult> GetImports()
        {
            try
            {
                return Ok(await _importService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}