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
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        public IEnumerable<Location> GetLocations()
        {
            return _locationRepository.GetAll();
        }
        public Guid CreateOrReturnExistingId(string location)
        {
            var existingLocation = GetLocations().FirstOrDefault(x => x.Name == location);

            if (existingLocation == null)
            {
                var newLocation = new Location
                {
                    Id = Guid.NewGuid(),
                    Name = location
                };

                _locationRepository.Insert(newLocation);

                return newLocation.Id;
            }

            return existingLocation.Id;
        }
    }
}
