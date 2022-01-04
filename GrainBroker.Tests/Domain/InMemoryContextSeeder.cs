using GrainBroker.Domain;
using GrainBroker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Tests.Domain
{
    public class InMemoryContextSeeder
    {
        Context _context;
        public InMemoryContextSeeder(Context context)
        {
            _context = context;
        }

        public void SeedContext()
        {
            var purchaseOrders = new PurchaseOrder
            {
                Id = new Guid("72bab322-2016-4cbd-91e6-16018b263118"),
                CustomerId = new Guid("aca585fc-6ab2-4356-9a96-ca251a296bf3"),
                SupplierId = new Guid("5658b3ed-107b-43ac-910c-46dfc69883f2"),
                DeliveryCost = 5,
                OrderDate = DateTime.Now,
                RequiredAmount = 10,
                SuppliedAmount = 10
            };
            var locations = new List<Location>
            {
                new Location
                {
                    Id = new Guid("91ea35a2-39d2-4e1d-a2c8-7ffb771b99ae"),
                    Name = "Penrith"
                },
                new Location
                {
                    Id = new Guid("cdf2766a-7618-4dd8-bdfd-9a5ee5436a8f"),
                    Name = "Carlisle"
                },
            };
            var customer = new Customer
            {
                Id = new Guid("aca585fc-6ab2-4356-9a96-ca251a296bf3"),
                LocationId = new Guid("91ea35a2-39d2-4e1d-a2c8-7ffb771b99ae")
            };
            var supplier = new Supplier
            {
                Id = new Guid("5658b3ed-107b-43ac-910c-46dfc69883f2"),
                LocationId = new Guid("cdf2766a-7618-4dd8-bdfd-9a5ee5436a8f")
            };
            _context.Location.AddRange(locations);
            _context.Customer.Add(customer);
            _context.Supplier.Add(supplier);
            _context.PurchaseOrder.Add(purchaseOrders);
            _context.SaveChanges();
        }
    }
}
