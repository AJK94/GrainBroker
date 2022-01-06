using GrainBroker.Controllers;
using GrainBroker.Domain;
using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GrainBroker.Tests.Domain
{
    public class PurchaseOrderRepositoryTests
    {
        Context _context;
        PurchaseOrderRepository _purchaseOrderRepository;
        InMemoryContextSeeder _contextSeeder;

        public PurchaseOrderRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
                 .EnableSensitiveDataLogging()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;
            _context = new Context(options);

            _purchaseOrderRepository = new PurchaseOrderRepository(_context);

            _contextSeeder = new InMemoryContextSeeder(_context);

            _contextSeeder.SeedContext();

        }

        [Fact]
        public void RepositoryConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new PurchaseOrderRepository(null));
        }

        [Fact]
        public async void GetAll()
        {
            var orders = await _purchaseOrderRepository.GetAll();
            Assert.Single(orders);
            Assert.Equal("Penrith", orders.FirstOrDefault()?.Customer.Location.Name);
            Assert.Equal("Carlisle", orders.FirstOrDefault()?.Supplier.Location.Name);
        }

        [Fact]
        public async void GetExistingById()
        {
            var order = await _purchaseOrderRepository.GetById(new Guid("72bab322-2016-4cbd-91e6-16018b263118"));

            Assert.Equal(5, order?.DeliveryCost);
        }

        [Fact]
        public async void GetNonExistingById()
        {
            var order = await _purchaseOrderRepository.GetById(Guid.NewGuid());

            Assert.Null(order);
        }

        [Fact]
        public async void Insert()
        {
            _purchaseOrderRepository.Insert(new PurchaseOrder
            {
                Id = Guid.NewGuid(),
                CustomerId = new Guid("aca585fc-6ab2-4356-9a96-ca251a296bf3"),
                SupplierId = new Guid("5658b3ed-107b-43ac-910c-46dfc69883f2"),
                DeliveryCost = 5,
                OrderDate = DateTime.Now,
                RequiredAmount = 10,
                SuppliedAmount = 10
            });
            var orders = await _purchaseOrderRepository.GetAll();
            Assert.Equal(2, orders.Count());
        }
    }
}