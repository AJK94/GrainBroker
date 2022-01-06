using CsvHelper.Configuration.Attributes;

namespace GrainBroker.Services.Models
{
    public class RawPurchaseOrderDTO
    {
        [Name("Order Date")]
        public DateTime OrderDate { get; set; }
        [Name("Purchase Order")]
        public Guid PurchaseOrder { get; set; }
        [Name("Customer ID")]
        public Guid CustomerId { get; set; }
        [Name("Customer Location")]
        public string CustomerLocation { get; set; }
        [Name("Order Req Amt (Ton)")]
        public int OrderReqAmtTon { get; set; }
        [Name("Fullfilled By ID")]
        public Guid FullfilledById { get; set; }
        [Name("Fullfilled By Location")]
        public string FullfilledByLocation { get; set; }
        [Name("Supplied Amt (Ton)")]
        public int SuppliedAmtTon { get; set; }
        [Name("Cost Of Delivery ($)")]
        public decimal CostOfDelivery { get; set; }
        [Ignore]
        public Guid ImportId { get; set; }
    }
}
