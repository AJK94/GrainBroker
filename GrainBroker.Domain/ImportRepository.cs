using GrainBroker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Repository
{
    public class ImportRepository : IRepository<Import>
    {
        private readonly Context _context;
        public ImportRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Import>> GetAll()
        {
            return await _context.Import.Include(x=> x.PurchaseOrders).OrderByDescending(x=> x.ImportDate).ToListAsync();
        }

        public async Task<Import?> GetById(Guid id)
        {
            return await _context.Import.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Import> Insert(Import import)
        {
            import.Id = Guid.NewGuid();
            await _context.Import.AddAsync(import);
            await _context.SaveChangesAsync();
            return import;
        }       
    }
}