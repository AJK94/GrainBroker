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
    public class SupplierServiceTests
    {
        private Mock<IRepository<Supplier>> _mockSupplierRepository;
        private Mock<ILocationService> _mockLocationService;
        private SupplierService _supplierService;

        public SupplierServiceTests()
        {
            _mockSupplierRepository = new();
            _mockLocationService = new();

            _supplierService = new(_mockSupplierRepository.Object, _mockLocationService.Object);
        }
        [Fact]
        public void ServiceConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new SupplierService(null, _mockLocationService.Object));
            Assert.Throws<ArgumentNullException>(() => new SupplierService(_mockSupplierRepository.Object, null));
    
        }

        [Fact]
        public async void GetSuppliers()
        {
            var expectedResult = new List<Supplier>{
                new Supplier {
                    Id = Guid.NewGuid(),
                    LocationId = Guid.NewGuid(),  
                }
            };

            _mockSupplierRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(expectedResult);

            var result = await _supplierService.GetSuppliers();

            Assert.Equal(expectedResult, result.ToList());
        }

        [Fact]
        public void Insert()
        {
            var expectedResult = new List<Supplier>{
                new Supplier {
                    Id = Guid.NewGuid(),
                    LocationId = Guid.NewGuid()
                }
            };

            _mockSupplierRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(expectedResult);

            _supplierService.CreateIfNotExist("Penrith", Guid.NewGuid());
        }

        [Fact]
        public async void Insert_Already_Exists()
        {
            var existingId = Guid.NewGuid();

            var expectedResult = new Supplier
            {
                Id = existingId,
                LocationId = Guid.NewGuid()

            };

            _mockSupplierRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(expectedResult);

            var result = await _supplierService.CreateIfNotExist("Penrith", existingId);

            Assert.Equal(existingId, result.Id);
        }
    }
}
