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
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly ILocationService _locationService;

        public SupplierService(IRepository<Supplier> supplierRepository, ILocationService locationService)
        {
            _supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _supplierRepository.GetAll();
        }

        public async Task<Supplier> CreateIfNotExist(string location, Guid id)
        {
            var existingSupplier = await _supplierRepository.GetById(id);

            if (existingSupplier == null)
            {
                var supplier = new Supplier
                {
                    Id = id,
                    LocationId = await _locationService.CreateOrReturnExistingId(location)
                };

                return await _supplierRepository.Insert(supplier);
            }
            return existingSupplier;
        }
    }
}