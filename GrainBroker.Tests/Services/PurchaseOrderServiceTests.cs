using GrainBroker.Controllers;
using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services;
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
        public async void GetPurchaseOrders()
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
                .ReturnsAsync(expectedResult);

            var result = await _purchaseOrderService.GetPurchaseOrders();

            Assert.Equal(expectedResult, result.ToList());
        }

        [Fact]
        public async void InsertNewPurchaseOrder()
        {
            var expectedInsert = new PurchaseOrder
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                SupplierId = Guid.NewGuid(),
                DeliveryCost = 5,
                OrderDate = DateTime.Now,
                RequiredAmount = 10,
                SuppliedAmount = 10
            };

            _mockPurchaseOrderRepository
                .Setup(x => x.Insert(It.IsAny<PurchaseOrder>()))
                .ReturnsAsync(expectedInsert);

            var insertedPurhcaseOrder = await _purchaseOrderService.Insert(new PurchaseOrderDTO
            {
                PurchaseOrderId = expectedInsert.Id,
                FullfilledById = expectedInsert.SupplierId,
                FullfilledByLocation = "Chicago",
                CustomerId = expectedInsert.CustomerId,
                CustomerLocation = "Texas",
                CostOfDelivery = 5,
                OrderDate = DateTime.Now,
                OrderReqAmtTon = 10,
                SuppliedAmtTon = 10
            });

            Assert.Equal(expectedInsert.Id, insertedPurhcaseOrder.Id);
        }

        [Fact]
        public async void InsertExistingPurchaseOrder()
        {
            var expectedOrder = new PurchaseOrder
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                SupplierId = Guid.NewGuid(),
                DeliveryCost = 5,
                OrderDate = DateTime.Now,
                RequiredAmount = 10,
                SuppliedAmount = 10
            };
           

            _mockPurchaseOrderRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(expectedOrder);

         
            var insertedPurhcaseOrder = await _purchaseOrderService.Insert(new PurchaseOrderDTO
            {
                PurchaseOrderId = expectedOrder.Id,
                FullfilledById = expectedOrder.SupplierId,
                FullfilledByLocation = "Chicago",
                CustomerId = expectedOrder.CustomerId,
                CustomerLocation = "Texas",
                CostOfDelivery = 5,
                OrderDate = DateTime.Now,
                OrderReqAmtTon = 10,
                SuppliedAmtTon = 10
            });

            Assert.Equal(expectedOrder.Id, insertedPurhcaseOrder.Id);
        }

        [Fact]
        public async void GetOrdersGroupedByCustomer()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Location = new Location
                {
                    Name = "Penrith"
                }

            };

            var expectedResult = new List<PurchaseOrder>{
                new PurchaseOrder {
                    Id = Guid.NewGuid(),
                    CustomerId = customer.Id,
                    SupplierId = Guid.NewGuid(),
                    DeliveryCost = 5,
                    OrderDate = DateTime.Now,
                    RequiredAmount = 10,
                    SuppliedAmount = 5,
                    Customer = customer
                },
                 new PurchaseOrder {
                    Id = Guid.NewGuid(),
                    CustomerId = customer.Id,
                    SupplierId = Guid.NewGuid(),
                    DeliveryCost = 5,
                    OrderDate = DateTime.Now,
                    RequiredAmount = 10,
                    SuppliedAmount = 5,
                    Customer = customer
                }
            };

            _mockPurchaseOrderRepository
            .Setup(x => x.GetAll())
            .ReturnsAsync(expectedResult);

            var ordersGrouped = await _purchaseOrderService.GetOrdersGroupedByCustomer();

            Assert.Single(ordersGrouped);
            var groupedOrdersForCustomer = ordersGrouped.FirstOrDefault();
            Assert.Equal(customer.Id, groupedOrdersForCustomer.CustomerId);
            Assert.Equal(10, groupedOrdersForCustomer.SuppliedAmountTotal);
            Assert.Equal(20, groupedOrdersForCustomer.RequiredAmountTotal);
        }

        [Fact]
        public async void GetOrdersGroupedBySupplier()
        {
            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                Location = new Location
                {
                    Name = "Penrith"
                }

            };

            var expectedResult = new List<PurchaseOrder>{
                new PurchaseOrder {
                    Id = Guid.NewGuid(),
                    SupplierId = supplier.Id,
                    CustomerId = Guid.NewGuid(),
                    DeliveryCost = 5,
                    OrderDate = DateTime.Now,
                    RequiredAmount = 10,
                    SuppliedAmount = 5,
                    Supplier = supplier
                },
                 new PurchaseOrder {
                    Id = Guid.NewGuid(),
                    SupplierId = supplier.Id,
                    CustomerId = Guid.NewGuid(),
                    DeliveryCost = 5,
                    OrderDate = DateTime.Now,
                    RequiredAmount = 10,
                    SuppliedAmount = 5,
                    Supplier = supplier
                }
            };

            _mockPurchaseOrderRepository
            .Setup(x => x.GetAll())
            .ReturnsAsync(expectedResult);

            var ordersGrouped = await _purchaseOrderService.GetOrdersGroupedBySupplier();

            Assert.Single(ordersGrouped);
            var groupedOrdersForSupplier = ordersGrouped.FirstOrDefault();
            Assert.Equal(supplier.Id, groupedOrdersForSupplier.SupplierId);
            Assert.Equal(10, groupedOrdersForSupplier.SuppliedAmountTotal);
            Assert.Equal(20, groupedOrdersForSupplier.RequiredAmountTotal);
        }
    }
}
