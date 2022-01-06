namespace GrainBroker.Services.Models
{
    public class PurchaseOrderDTO
    {
        public DateTime OrderDate { get; set; }
        public Guid ImportId { get; set; }    
        public Guid PurchaseOrderId { get; set; }
        public Guid CustomerId { get; set; }        
        public string CustomerLocation { get; set; }
        public int OrderReqAmtTon { get; set; }
        public Guid FullfilledById { get; set; }
        public string FullfilledByLocation { get; set; }
        public int SuppliedAmtTon { get; set; }
        public decimal CostOfDelivery { get; set; }
    }
}
