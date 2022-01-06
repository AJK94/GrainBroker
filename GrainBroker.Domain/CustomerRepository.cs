using GrainBroker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Repository
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly Context _context;
        public CustomerRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer?> GetById(Guid id)
        {
            return await _context.Customer.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> Insert(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}