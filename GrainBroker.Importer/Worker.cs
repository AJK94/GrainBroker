using CsvHelper;
using GrainBroker.Domain;
using GrainBroker.Domain.Models;
using GrainBroker.Services.Interfaces;
using System.Globalization;

namespace GrainBroker.Importer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IPurchaseOrderService _purchaseOrderService;

        public Worker(ILogger<Worker> logger, IPurchaseOrderService purchaseOrderService)
        {
            _logger = logger;
            _purchaseOrderService = purchaseOrderService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            using (var reader = new StreamReader("ImportData/dataset.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<RawOrder>().ToList();

                foreach (var record in records)
                {
                    _purchaseOrderService.Insert(new Services.Models.PurchaseOrder
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
                }
             
                _logger.LogInformation($"{records.Count} purchase orders imported.");
            }
        }
    }
}