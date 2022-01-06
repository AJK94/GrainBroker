using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Models
{
    public class PurchaseOrder
    {
        public Guid Id { get; set; }
        public Guid ImportId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
        public int RequiredAmount { get; set; }
        public int SuppliedAmount { get; set; }
        public decimal DeliveryCost { get; set; }
        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
    }
}
