using GrainBroker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Repository
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private readonly Context _context;
        public SupplierRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Supplier.AsEnumerable();
        }

        public void Insert(Supplier supplier)
        {
            _context.Supplier.Add(supplier);
            _context.SaveChanges();
        }       
    }
}