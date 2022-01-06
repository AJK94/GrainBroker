using CsvHelper;
using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services.Interfaces;
using GrainBroker.Services.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace GrainBroker.Services
{
    public class ImportService : IImportService
    {

        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IRepository<Import> _importRepository;
        private readonly ILogger _logger;

        public ImportService(IPurchaseOrderService purchaseOrderService,
                             IRepository<Import> importRepository,
                             ILogger<IImportService> logger)
        {
            _purchaseOrderService = purchaseOrderService ?? throw new ArgumentNullException(nameof(purchaseOrderService));
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> ImportPurchaseOrderCsv(StreamReader stream, string fileName)
        {
            using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
                try
                {
                    var import = await Insert(new Import
                    {
                        FileName = fileName,
                        ImportDate = DateTime.Now,
                    });

                    var records = csv.GetRecords<RawPurchaseOrderDTO>().ToList();

                    foreach (var record in records)
                    {
                        var order = await _purchaseOrderService.Insert(
                            new PurchaseOrderDTO
                            {
                                CostOfDelivery = record.CostOfDelivery,
                                CustomerId = record.CustomerId,
                                CustomerLocation = record.CustomerLocation,
                                FullfilledById = record.FullfilledById,
                                FullfilledByLocation = record.FullfilledByLocation,
                                OrderDate = record.OrderDate,
                                OrderReqAmtTon = record.OrderReqAmtTon,
                                PurchaseOrderId = record.PurchaseOrder,
                                SuppliedAmtTon = record.SuppliedAmtTon,
                                ImportId = import.Id,
                            }
                        );
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
        public async Task<Import> Insert(Import import)
        {
            return await _importRepository.Insert(import);
        }

        public async Task<IEnumerable<Import>> GetAll()
        {
            return await _importRepository.GetAll();
        }
    }
}