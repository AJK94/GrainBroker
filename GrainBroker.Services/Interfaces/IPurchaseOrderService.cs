using GrainBroker.Domain.Models;

namespace GrainBroker.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        /// <summary>
        /// Gets all purchase orders
        /// </summary>
        /// <returns>An IEnumerable of PurchaseOrder</returns>
        IEnumerable<PurchaseOrder> GetPurchaseOrders();
        /// <summary>
        /// Insert a new purhcase order
        /// </summary>
        /// <param name="purchaseOrder">The new purchase order</param>
        void Insert(Models.PurchaseOrder purchaseOrder);
    }
}
