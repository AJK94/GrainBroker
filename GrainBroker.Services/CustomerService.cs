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
        public IEnumerable<Customer> GetCustomers() {
            return _customerRepository.GetAll();
        }

        public void CreateIfNotExist(string location, Guid id)
        {
            var existingCustomer = GetCustomers().FirstOrDefault(x => x.Id == id);

            if (existingCustomer == null)
            {
                var customer = new Customer
                {
                    Id = id,
                    LocationId = _locationService.CreateOrReturnExistingId(location)
                };

                _customerRepository.Insert(customer);
            }

        }
    }
}
