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
    public class CustomerRepositoryTests
    {
        Context _context;
        CustomerRepository _customerRepository;
        InMemoryContextSeeder _contextSeeder;

        public CustomerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
                 .EnableSensitiveDataLogging()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;
            _context = new Context(options);

            _customerRepository = new CustomerRepository(_context);

            _contextSeeder = new InMemoryContextSeeder(_context);

            _contextSeeder.SeedContext();

        }

        [Fact]
        public void RepositoryConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerRepository(null));
        }

        [Fact]
        public async void GetAll()
        {
            var customers = await _customerRepository.GetAll();
            Assert.Single(customers);
            Assert.Equal("Penrith", customers.FirstOrDefault()?.Location.Name);
        }

        [Fact]
        public async void GetExistingById()
        {
            var customer = await _customerRepository.GetById(new Guid("aca585fc-6ab2-4356-9a96-ca251a296bf3"));
      
            Assert.Equal(new Guid("91ea35a2-39d2-4e1d-a2c8-7ffb771b99ae"), customer?.LocationId);
        }

        [Fact]
        public async void GetNonExistingById()
        {
            var customer = await _customerRepository.GetById(Guid.NewGuid());

            Assert.Null(customer);
        }

        [Fact]
        public async void Insert()
        {
            _customerRepository.Insert(new Customer
            {
                Id = Guid.NewGuid(),
                LocationId = new Guid("91ea35a2-39d2-4e1d-a2c8-7ffb771b99ae")
            }); 
            var customers = await _customerRepository.GetAll();
            Assert.Equal(2, customers.Count());
        }
    }
}