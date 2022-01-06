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

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrders()
        {
            return await _purchaseOrderRepository.GetAll();
        }

        public async Task<PurchaseOrder> Insert(Models.PurchaseOrderDTO purchaseOrder)
        {      
            var existingOrder = await _purchaseOrderRepository.GetById(purchaseOrder.PurchaseOrderId);

            if (existingOrder == null)
            {
                await _supplierService.CreateIfNotExist(purchaseOrder.FullfilledByLocation, purchaseOrder.FullfilledById);

                await _customerService.CreateIfNotExist(purchaseOrder.CustomerLocation, purchaseOrder.CustomerId);

               return await _purchaseOrderRepository.Insert(ToDomain(purchaseOrder));
            }
            return existingOrder;
        }

        private static PurchaseOrder ToDomain(Models.PurchaseOrderDTO purchaseOrder)
        {
            return new PurchaseOrder()
            {
                Id = purchaseOrder.PurchaseOrderId,
                CustomerId = purchaseOrder.CustomerId,
                SupplierId = purchaseOrder.FullfilledById,
                RequiredAmount = purchaseOrder.OrderReqAmtTon,
                SuppliedAmount = purchaseOrder.SuppliedAmtTon,
                DeliveryCost = purchaseOrder.CostOfDelivery,
                OrderDate = purchaseOrder.OrderDate,
                ImportId = purchaseOrder.ImportId
            };
        }

        public async Task<IEnumerable<Models.PurchaseOrderGroupedByCustomerDTO>> GetOrdersGroupedByCustomer()
        {
            var allOrders = await _purchaseOrderRepository.GetAll();

            return allOrders.GroupBy(x => x.CustomerId).Select(x => new Models.PurchaseOrderGroupedByCustomerDTO
            {
                CustomerId = x.FirstOrDefault().CustomerId,
                CustomerLocation = x.FirstOrDefault().Customer.Location.Name,
                CostOfDeliveryTotal = x.Sum(s=> s.DeliveryCost),
                RequiredAmountTotal = x.Sum(s => s.RequiredAmount),
                SuppliedAmountTotal = x.Sum(s => s.SuppliedAmount)
            }); 
        }

        public async Task<IEnumerable<Models.PurchaseOrderGroupedBySupplierDTO>> GetOrdersGroupedBySupplier()
        {
            var allOrders = await _purchaseOrderRepository.GetAll();

            return allOrders.GroupBy(x => x.SupplierId).Select(x => new Models.PurchaseOrderGroupedBySupplierDTO
            {
                SupplierId = x.FirstOrDefault().SupplierId,
                SupplierLocation = x.FirstOrDefault().Supplier.Location.Name,
                CostOfDeliveryTotal = x.Sum(s => s.DeliveryCost),
                RequiredAmountTotal = x.Sum(s => s.RequiredAmount),
                SuppliedAmountTotal = x.Sum(s => s.SuppliedAmount)
            });
        }
    }
}