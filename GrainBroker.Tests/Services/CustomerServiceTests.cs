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
    public class CustomerServiceTests
    {
        private Mock<IRepository<Customer>> _mockCustomerRepository;
        private Mock<ILocationService> _mockLocationService;
        private CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockCustomerRepository = new();
            _mockLocationService = new();

            _customerService = new(_mockCustomerRepository.Object, _mockLocationService.Object);
        }
        [Fact]
        public void ServiceConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerService(null, _mockLocationService.Object));
            Assert.Throws<ArgumentNullException>(() => new CustomerService(_mockCustomerRepository.Object, null));

        }

        [Fact]
        public async void GetCustomers()
        {
            var expectedResult = new List<Customer>{
                new Customer {
                    Id = Guid.NewGuid(),
                    LocationId = Guid.NewGuid(),
                }
            };

            _mockCustomerRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(expectedResult);

            var result = await _customerService.GetCustomers();

            Assert.Equal(expectedResult, result.ToList());
        }

        [Fact]
        public async void Insert()
        {

            var toInsertId = Guid.NewGuid();

            var expectedInsert = new Customer
            {
                Id = toInsertId,
                LocationId = Guid.NewGuid(),
            };

            _mockCustomerRepository.Setup(x => x.Insert(It.IsAny<Customer>())).ReturnsAsync(expectedInsert);


            var inserted = await _customerService.CreateIfNotExist("Penrith", toInsertId);

            Assert.Equal(toInsertId, inserted.Id);
        }
        [Fact]
        public async void Insert_Already_Exists()
        {
            var existingId = Guid.NewGuid();

            var expectedResult = new Customer
            {
                Id = existingId,
                LocationId = Guid.NewGuid()

            };

            _mockCustomerRepository
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(expectedResult);

            var result = await _customerService.CreateIfNotExist("Penrith", existingId);

            Assert.Equal(existingId, result.Id);
        }
    }
}
