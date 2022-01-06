namespace GrainBroker.Services.Models
{
    public class PurchaseOrderGroupedByCustomerDTO
    {
        public Guid CustomerId { get; set; }
        public string CustomerLocation { get; set; }
        public int RequiredAmountTotal { get; set; }
        public int SuppliedAmountTotal { get; set; }
        public decimal CostOfDeliveryTotal { get; set; }
    }
}
