using GrainBroker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Repository
{
    public class LocationRepository : IRepository<Location>
    {
        private readonly Context _context;
        public LocationRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Location.AsEnumerable();
        }

        public void Insert(Location location)
        {
            _context.Location.Add(location);
            _context.SaveChanges();
        }
    }
}