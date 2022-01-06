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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ILocationService _locationService;

        public CustomerService(IRepository<Customer> customerRepository, ILocationService locationService)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        }
        public async Task<IEnumerable<Customer>> GetCustomers() {
            return await _customerRepository.GetAll();
        }

        public async Task<Customer> CreateIfNotExist(string location, Guid id)
        {
            var existingCustomer = await _customerRepository.GetById(id);

            if (existingCustomer == null)
            {
                var customer = new Customer
                {
                    Id = id,
                    LocationId = await _locationService.CreateOrReturnExistingId(location)
                };

                return await _customerRepository.Insert(customer);
            }

            return existingCustomer;

        }
    }
}
