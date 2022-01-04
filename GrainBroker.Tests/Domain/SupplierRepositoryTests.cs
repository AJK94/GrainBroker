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
    public class SupplierRepositoryTests
    {
        Context _context;
        SupplierRepository _supplierRepository;
        InMemoryContextSeeder _contextSeeder;

        public SupplierRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
                 .EnableSensitiveDataLogging()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;
            _context = new Context(options);

            _supplierRepository = new SupplierRepository(_context);

            _contextSeeder = new InMemoryContextSeeder(_context);

            _contextSeeder.SeedContext();

        }

        [Fact]
        public void RepositoryConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new SupplierRepository(null));
        }

        [Fact]
        public void GetAll()
        {
            var suppliers = _supplierRepository.GetAll();
            Assert.Single(suppliers);
            Assert.Equal("Carlisle", suppliers.FirstOrDefault().Location.Name);
        }

        [Fact]
        public void Insert()
        {
            _supplierRepository.Insert(new Supplier
            {
                Id = Guid.NewGuid(),
                LocationId = new Guid("91ea35a2-39d2-4e1d-a2c8-7ffb771b99ae")
            }); 
            var suppliers = _supplierRepository.GetAll();
            Assert.Equal(2, suppliers.Count());
        }
    }
}