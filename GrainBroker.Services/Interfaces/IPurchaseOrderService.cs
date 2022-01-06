using GrainBroker.Domain.Models;

namespace GrainBroker.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        /// <summary>
        /// Gets all purchase orders
        /// </summary>
        /// <returns>An IEnumerable of PurchaseOrder</returns>
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrders();

        /// <summary>
        /// Insert a new purhcase order
        /// </summary>
        /// <param name="purchaseOrder">The new purchase order</param>
        Task<PurchaseOrder> Insert(Models.PurchaseOrderDTO purchaseOrder);

        /// <summary>
        /// Get all orders grouped by customer 
        /// </summary>
        /// <returns>An IEnumerable of PurchaseOrderGroupedByCustomer</returns>
        Task<IEnumerable<Models.PurchaseOrderGroupedByCustomerDTO>> GetOrdersGroupedByCustomer();

        /// <summary>
        /// Get all orders grouped by customer 
        /// </summary>
        /// <returns>An IEnumerable of PurchaseOrderGroupedByCustomer</returns>
        Task<IEnumerable<Models.PurchaseOrderGroupedBySupplierDTO>> GetOrdersGroupedBySupplier();
    }
}
