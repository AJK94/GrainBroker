using GrainBroker.Domain;
using GrainBroker.Domain.Models;
using GrainBroker.Services;
using GrainBroker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrainBroker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ILogger<PurchaseOrderController> _logger;
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderController(ILogger<PurchaseOrderController> logger, IPurchaseOrderService purchaseOrderService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _purchaseOrderService = purchaseOrderService ?? throw new ArgumentNullException(nameof(purchaseOrderService));
        }

        [HttpGet("GetPurchaseOrders")]
        public async Task<IActionResult> GetPurchaseOrders()
        {
            try
            {
                return Ok(await _purchaseOrderService.GetPurchaseOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("GetOrdersGroupedByCustomer")]
        public async Task<IActionResult> GetOrdersGroupedByCustomer()
        {
            try
            {
                return Ok(await _purchaseOrderService.GetOrdersGroupedByCustomer());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetOrdersGroupedBySupplier")]
        public async Task<IActionResult> GetOrdersGroupedBySupplier()
        {
            try
            {
                return Ok(await _purchaseOrderService.GetOrdersGroupedBySupplier());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}