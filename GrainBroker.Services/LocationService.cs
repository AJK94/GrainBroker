using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await _locationRepository.GetAll();
        }

        public async Task<Guid> CreateOrReturnExistingId(string location)
        {
            var existingLocation = await _locationRepository.GetByLocation(location);

            if (existingLocation == null)
            {
                var newLocation = new Location
                {
                    Id = Guid.NewGuid(),
                    Name = location
                };

                newLocation = await _locationRepository.Insert(newLocation);

                return newLocation.Id;
            }

            return existingLocation.Id;
        }
    }
}
