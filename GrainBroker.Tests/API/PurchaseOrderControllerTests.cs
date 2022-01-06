using GrainBroker.Controllers;
using GrainBroker.Domain.Models;
using GrainBroker.Services.Interfaces;
using GrainBroker.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GrainBroker.Tests.API
{
    public class PurchaseOrderControllerTests
    {
        private Mock<ILogger<PurchaseOrderController>> _mockLogger;
        private Mock<IPurchaseOrderService> _mockPurchaseOrderService;
        private PurchaseOrderController _purchaseOrderController;

        public PurchaseOrderControllerTests()
        {
            _mockLogger = new();
            _mockPurchaseOrderService = new();
            _purchaseOrderController = new(_mockLogger.Object, _mockPurchaseOrderService.Object);

        }
        [Fact]
        public void ControllerConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new PurchaseOrderController(null, _mockPurchaseOrderService.Object));
            Assert.Throws<ArgumentNullException>(() => new PurchaseOrderController(_mockLogger.Object, null));
        }

        [Fact]
        public async void GetPurchaseOrders_Handles_Exception()
        {
            _mockPurchaseOrderService
                 .Setup(pos => pos.GetPurchaseOrders())
                 .Throws(new Exception());

            var result = await _purchaseOrderController.GetPurchaseOrders();

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result);
            var actualResult = (StatusCodeResult)result;
            Assert.Equal(StatusCodes.Status500InternalServerError, actualResult.StatusCode);
        }

        [Fact]
        public async void GetPurchaseOrders_Returns_Values()
        {
            var expectedResult = new List<PurchaseOrder>{
                new PurchaseOrder {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    SupplierId = Guid.NewGuid(),
                    DeliveryCost = 5,
                    OrderDate = DateTime.Now,
                    RequiredAmount = 10,
                    SuppliedAmount = 10
                }
            };
            
            _mockPurchaseOrderService
                 .Setup(pos => pos.GetPurchaseOrders())
                 .ReturnsAsync(expectedResult);

            var result = await _purchaseOrderController.GetPurchaseOrders();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.Equal(expectedResult, actualResult.Value);
        }

        [Fact]
        public async void GetOrdersGroupedByCustomer_Handles_Exception()
        {
            _mockPurchaseOrderService
                 .Setup(pos => pos.GetOrdersGroupedByCustomer())
                 .Throws(new Exception());

            var result = await _purchaseOrderController.GetOrdersGroupedByCustomer();

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result);
            var actualResult = (StatusCodeResult)result;
            Assert.Equal(StatusCodes.Status500InternalServerError, actualResult.StatusCode);
        }

        [Fact]
        public async void GetOrdersGroupedByCustomer_Returns_Value()
        {
            var expectedResult = new List<PurchaseOrderGroupedByCustomerDTO>{
                new PurchaseOrderGroupedByCustomerDTO {
                  CustomerId = Guid.NewGuid(),
                  CustomerLocation = "Penrith",
                  CostOfDeliveryTotal = 10,
                  RequiredAmountTotal = 10,
                  SuppliedAmountTotal = 10
                }
            };

            _mockPurchaseOrderService
                 .Setup(pos => pos.GetOrdersGroupedByCustomer())
                 .ReturnsAsync(expectedResult);

            var result = await _purchaseOrderController.GetOrdersGroupedByCustomer();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.Equal(expectedResult, actualResult.Value);
        }


        [Fact]
        public async void GetOrdersGroupedBySupplier_Handles_Exception()
        {
            _mockPurchaseOrderService
                 .Setup(pos => pos.GetOrdersGroupedBySupplier())
                 .Throws(new Exception());

            var result = await _purchaseOrderController.GetOrdersGroupedBySupplier();

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result);
            var actualResult = (StatusCodeResult)result;
            Assert.Equal(StatusCodes.Status500InternalServerError, actualResult.StatusCode);
        }

        [Fact]
        public async void GetOrdersGroupedBySupplier_Returns_Value()
        {
            var expectedResult = new List<PurchaseOrderGroupedBySupplierDTO>{
                new PurchaseOrderGroupedBySupplierDTO {
                  SupplierId = Guid.NewGuid(),
                  SupplierLocation = "Penrith",
                  CostOfDeliveryTotal = 10,
                  RequiredAmountTotal = 10,
                  SuppliedAmountTotal = 10
                }
            };

            _mockPurchaseOrderService
                 .Setup(pos => pos.GetOrdersGroupedBySupplier())
                 .ReturnsAsync(expectedResult);

            var result = await _purchaseOrderController.GetOrdersGroupedBySupplier();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.Equal(expectedResult, actualResult.Value);
        }
    }
}
