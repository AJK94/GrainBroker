namespace GrainBroker.Services.Models
{
    public class PurchaseOrderGroupedBySupplierDTO
    {
        public Guid SupplierId { get; set; }
        public string SupplierLocation { get; set; }
        public int RequiredAmountTotal { get; set; }
        public int SuppliedAmountTotal { get; set; }
        public decimal CostOfDeliveryTotal { get; set; }
    }
}
