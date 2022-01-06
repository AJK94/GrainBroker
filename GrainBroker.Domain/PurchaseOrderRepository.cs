using GrainBroker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Repository
{
    public class PurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        private readonly Context _context;
        public PurchaseOrderRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAll()
        {
            return await _context.PurchaseOrder
                .Include(x => x.Customer)
                .ThenInclude(x => x.Location)
                .Include(x => x.Supplier)
                .ThenInclude(x => x.Location)
                .ToListAsync();
        }

        public async Task<PurchaseOrder?> GetById(Guid id)
        {
            return await _context.PurchaseOrder.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PurchaseOrder> Insert(PurchaseOrder purchaseOrder)
        {
            await _context.PurchaseOrder.AddAsync(purchaseOrder);
            await _context.SaveChangesAsync();
            return purchaseOrder;
        }       
    }
}