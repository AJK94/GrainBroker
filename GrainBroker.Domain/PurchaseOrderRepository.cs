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

        public IEnumerable<PurchaseOrder> GetAll()
        {
            return _context.PurchaseOrder
                .Include(x => x.Customer)
                .ThenInclude(x => x.Location)
                .Include(x => x.Supplier)
                .ThenInclude(x => x.Location)
                .AsEnumerable();
        }

        public void Insert(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrder.Add(purchaseOrder);
            _context.SaveChanges();
        }       
    }
}