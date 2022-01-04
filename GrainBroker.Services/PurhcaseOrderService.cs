using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services.Interfaces;

namespace GrainBroker.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IRepository<PurchaseOrder> _purchaseOrderRepository;
        private readonly ISupplierService _supplierService;
        private readonly ICustomerService _customerService;

        public PurchaseOrderService(IRepository<PurchaseOrder> purchaseOrderRepository, ISupplierService supplierService, ICustomerService customerService)
        {
            _purchaseOrderRepository = purchaseOrderRepository ?? throw new ArgumentNullException(nameof(purchaseOrderRepository));
            _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            return _purchaseOrderRepository.GetAll();
        }

        public void Insert(Models.PurchaseOrder purchaseOrder)
        {      
            var existingOrder = GetPurchaseOrders().FirstOrDefault(x => x.Id == purchaseOrder.PurchaseOrderId);

            if (existingOrder == null)
            {
                _supplierService.CreateIfNotExist(purchaseOrder.FullfilledByLocation, purchaseOrder.FullfilledByID);

                _customerService.CreateIfNotExist(purchaseOrder.CustomerLocation, purchaseOrder.CustomerID);

                _purchaseOrderRepository.Insert(ToDomain(purchaseOrder));
            }
        }

        private static PurchaseOrder ToDomain(Models.PurchaseOrder purchaseOrder)
        {
            return new PurchaseOrder()
            {
                Id = purchaseOrder.PurchaseOrderId,
                CustomerId = purchaseOrder.CustomerID,
                SupplierId = purchaseOrder.FullfilledByID,
                RequiredAmount = purchaseOrder.OrderReqAmtTon,
                SuppliedAmount = purchaseOrder.SuppliedAmtTon,
                DeliveryCost = purchaseOrder.CostOfDelivery,
                OrderDate = purchaseOrder.OrderDate
            };
        }
    }
}