using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Models
{
    public class Import 
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }    
        public DateTime ImportDate { get; set; }  
        public List<PurchaseOrder> PurchaseOrders { get; set; }

    }
}