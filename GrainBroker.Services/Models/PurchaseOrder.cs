namespace GrainBroker.Services.Models
{
    public class PurchaseOrder
    {
        public DateTime OrderDate { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid CustomerID { get; set; }
     
        public string CustomerLocation { get; set; }
        public int OrderReqAmtTon { get; set; }
        public Guid FullfilledByID { get; set; }
        public string FullfilledByLocation { get; set; }
        public int SuppliedAmtTon { get; set; }
        public decimal CostOfDelivery { get; set; }
    }
}
