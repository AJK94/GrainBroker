using GrainBroker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly Context _context;
        public LocationRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _context.Location.ToListAsync();
        }

        public async Task<Location?> GetById(Guid id)
        {
            return await _context.Location.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Location?> GetByLocation(string name)
        {
            return await _context.Location.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Location> Insert(Location location)
        {
            await _context.Location.AddAsync(location);
            await _context.SaveChangesAsync();
            return location; 
        }
    }
}