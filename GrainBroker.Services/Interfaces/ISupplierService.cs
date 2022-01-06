using GrainBroker.Domain.Models;

namespace GrainBroker.Services.Interfaces
{
    public interface ISupplierService
    {
        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>An IEnumerable of suppliers</returns>
        Task<IEnumerable<Supplier>> GetSuppliers();

        /// <summary>
        /// Creates a new supplier 
        /// </summary>
        /// <param name="location">The supplier location</param>
        /// <param name="id">The supplier id</param>
        Task<Supplier> CreateIfNotExist(string location, Guid id);
    }
}
