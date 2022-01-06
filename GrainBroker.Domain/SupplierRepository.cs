using GrainBroker.Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _context.Supplier.ToListAsync();
        }

        public async Task<Supplier?> GetById(Guid id)
        {
            return await _context.Supplier.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Supplier> Insert(Supplier supplier)
        {
            await _context.Supplier.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }       
    }
}