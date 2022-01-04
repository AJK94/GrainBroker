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
        public void GetCustomers()
        {
            var expectedResult = new List<Customer>{
                new Customer {
                    Id = Guid.NewGuid(),
                    LocationId = Guid.NewGuid(),  
                }
            };

            _mockCustomerRepository
                .Setup(x => x.GetAll())
                .Returns(expectedResult);

            var result = _customerService.GetCustomers();

            Assert.Equal(expectedResult, result.ToList());
        }

        [Fact]
        public void Insert()
        {
            var expectedResult = new List<Customer>{
                new Customer {
                    Id = Guid.NewGuid(),
                    LocationId = Guid.NewGuid()
                }
            };

            _mockCustomerRepository
                .Setup(x => x.GetAll())
                .Returns(expectedResult);

            _customerService.CreateIfNotExist("Penrith", Guid.NewGuid());
        }
    }
}
