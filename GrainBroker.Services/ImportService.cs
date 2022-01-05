using CsvHelper;
using GrainBroker.Services.Interfaces;
using GrainBroker.Services.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace GrainBroker.Services
{
    public class ImportService : IImportService
    {

        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ILogger _logger;

        public ImportService(IPurchaseOrderService purchaseOrderService, ILogger<IImportService> logger)
        {
            _purchaseOrderService = purchaseOrderService ?? throw new ArgumentNullException(nameof(purchaseOrderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> ImportPurchaseOrderCsv(StreamReader stream)
        {
            using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
            {               
                try
                {
                    var records = csv.GetRecords<RawPurhcaseOrder>().ToList();

                    foreach (var record in records)
                    {
                        _purchaseOrderService.Insert(new Models.PurchaseOrder
                        {
                            CostOfDelivery = record.CostOfDelivery,
                            CustomerID = record.CustomerID,
                            CustomerLocation = record.CustomerLocation,
                            FullfilledByID = record.FullfilledByID,
                            FullfilledByLocation = record.FullfilledByLocation,
                            OrderDate = record.OrderDate,
                            OrderReqAmtTon = record.OrderReqAmtTon,
                            PurchaseOrderId = record.PurchaseOrder,
                            SuppliedAmtTon = record.SuppliedAmtTon
                        });
                        _logger.LogInformation($"test");

                    }

                    return records.Count;
                }
                catch (Exception ex)
                { 
                    _logger.LogError($"Error converting CSV to list: {ex.Message}.");
                    return 0;
                }
            }
        }
    }
}