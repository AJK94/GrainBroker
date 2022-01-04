using GrainBroker.Controllers;
using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services;
using GrainBroker.Services.Interfaces;
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

namespace GrainBroker.Tests.Services
{
    public class PurchaseOrderServiceTests
    {
        private Mock<IRepository<PurchaseOrder>> _mockPurchaseOrderRepository;
        private Mock<ISupplierService> _mockSupplierService;
        private Mock<ICustomerService> _mockCustomerService;
        private PurchaseOrderService _purchaseOrderService;

        public PurchaseOrderServiceTests()
        {
            _mockPurchaseOrderRepository = new();
            _mockSupplierService = new();
            _mockCustomerService = new();

            _purchaseOrderService = new(_mockPurchaseOrderRepository.Object, _mockSupplierService.Object, _mockCustomerService.Object);
        }
        [Fact]
        public void ServiceConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new PurchaseOrderService(null, _mockSupplierService.Object, _mockCustomerService.Object));
            Assert.Throws<ArgumentNullException>(() => new PurchaseOrderService(_mockPurchaseOrderRepository.Object, null, _mockCustomerService.Object));
            Assert.Throws<ArgumentNullException>(() => new PurchaseOrderService(_mockPurchaseOrderRepository.Object, _mockSupplierService.Object, null));
        }

        [Fact]
        public void GetPurchaseOrders()
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

            _mockPurchaseOrderRepository
                .Setup(x => x.GetAll())
                .Returns(expectedResult);

            var result = _purchaseOrderService.GetPurchaseOrders();

            Assert.Equal(expectedResult, result.ToList());
        }

        [Fact]
        public void InsertNewPurchaseOrder()
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

            _mockPurchaseOrderRepository
                .Setup(x => x.GetAll())
                .Returns(expectedResult);

            _purchaseOrderService.Insert(new GrainBroker.Services.Models.PurchaseOrder
            {
                PurchaseOrderId = Guid.NewGuid(),
                FullfilledByID = Guid.NewGuid(),
                FullfilledByLocation = "Chicago",
                CustomerID = Guid.NewGuid(),
                CustomerLocation= "Texas",
                CostOfDelivery = 5,
                OrderDate = DateTime.Now,
                OrderReqAmtTon = 10,
                SuppliedAmtTon = 10
            });
        }
    }
}
