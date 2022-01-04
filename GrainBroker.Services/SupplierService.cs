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

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _supplierRepository.GetAll();
        }

        public void CreateIfNotExist(string location, Guid id)
        {
            var existingSupplier = GetSuppliers().FirstOrDefault(x => x.Id == id);

            if (existingSupplier == null)
            {
                var supplier = new Supplier
                {
                    Id = id,
                    LocationId = _locationService.CreateOrReturnExistingId(location)
                };

                _supplierRepository.Insert(supplier);
            }
        }
    }
}