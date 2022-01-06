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
    public class LocationServiceTests
    {
        private Mock<ILocationRepository> _mockLocationRepository;
        private LocationService _locationService;

        public LocationServiceTests()
        {
            _mockLocationRepository = new();       

            _locationService = new(_mockLocationRepository.Object);
        }
        [Fact]
        public void ServiceConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new LocationService(null));
    
        }

        [Fact]
        public async void GetLocations()
        {
            var expectedResult = new List<Location>{
                new Location {
                    Id = Guid.NewGuid(),
                    Name = "Penrith"
                }
            };

            _mockLocationRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(expectedResult);

            var result = await _locationService.GetLocations();

            Assert.Equal(expectedResult, result.ToList());
        }

        [Fact]
        public async void CreateOrReturnExistingId_NewLocation()
        {
            var existingLocation = new Location
            {
                Id = Guid.NewGuid(),
                Name = "Penrith"
            };

            var newLocation = new Location
            {
                Id= Guid.NewGuid(),
                Name = "Carlisle"
            };

            var expectedResult = new List<Location>{
               existingLocation
            };

            _mockLocationRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(expectedResult);

            _mockLocationRepository
             .Setup(x => x.Insert(It.IsAny<Location>()))
             .ReturnsAsync(newLocation);

            var locationId = await _locationService.CreateOrReturnExistingId(newLocation.Name);

            Assert.NotEqual(existingLocation.Id, locationId);
        }

        [Fact]
        public async void CreateOrReturnExistingId_ExistingLocation()
        {
            var existingLocation = new Location
            {
                Id = Guid.NewGuid(),
                Name = "Penrith"
            };          

            _mockLocationRepository
                .Setup(x => x.GetByLocation(It.IsAny<string>()))
                .ReturnsAsync(existingLocation);

            var locationId = await _locationService.CreateOrReturnExistingId("Penrith");

            Assert.Equal(existingLocation.Id, locationId);
        }
    }
}
