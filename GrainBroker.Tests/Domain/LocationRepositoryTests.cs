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
    public class LocationRepositoryTests
    {
        Context _context;
        LocationRepository _locationRepository;
        InMemoryContextSeeder _contextSeeder;

        public LocationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
                 .EnableSensitiveDataLogging()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;
            _context = new Context(options);

            _locationRepository = new LocationRepository(_context);

            _contextSeeder = new InMemoryContextSeeder(_context);

            _contextSeeder.SeedContext();

        }

        [Fact]
        public void RepositoryConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new LocationRepository(null));
        }

        [Fact]
        public void GetAll()
        {
            var locations = _locationRepository.GetAll();
            Assert.Equal(2, locations.Count());
        }

        [Fact]
        public void Insert()
        {
            _locationRepository.Insert(new Location
            {
                Id = Guid.NewGuid(),
                Name = "Kendal"
            });
            var locations = _locationRepository.GetAll();
            Assert.Equal(3, locations.Count());
        }
    }
}