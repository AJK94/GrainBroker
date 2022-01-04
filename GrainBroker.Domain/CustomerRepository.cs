using GrainBroker.Domain.Models;
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

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer.AsEnumerable();
        }

        public void Insert(Customer customer)
        {
            _context.Customer.Add(customer);
            _context.SaveChanges();
        }
    }
}