using GrainBroker.Domain.Models;

namespace GrainBroker.Services.Interfaces
{
    public interface ILocationService
    {
        /// <summary>
        /// Gets all locations
        /// </summary>
        /// <returns>An IEnumerable of locations</returns>
        Task<IEnumerable<Location>> GetLocations();

        /// <summary>
        /// Creates a new location or returns the Id of an already created location
        /// </summary>
        /// <param name="location">The new location name</param>
        /// <returns>The Id of the new or created location</returns>
        Task<Guid> CreateOrReturnExistingId(string location);
    }
}