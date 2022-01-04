using GrainBroker.Domain.Models;

namespace GrainBroker.Services.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>An IEnumerable of customers</returns>
        IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Creates a new customer or returns the Id of an already created customer
        /// </summary>
        /// <param name="location">The customer location</param>
        /// <param name="id">The customer id</param>
        void CreateIfNotExist(string location, Guid id);
    }
}
