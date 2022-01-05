using GrainBroker.Services.Interfaces;

namespace GrainBroker.Importer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            using (var reader = new StreamReader("ImportData/dataset.csv"))
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IImportService importService =
                        scope.ServiceProvider.GetRequiredService<IImportService>();

                    var recordCount = await importService.ImportPurchaseOrderCsv(reader);

                    _logger.LogInformation($"{recordCount} purchase orders imported.");
                }
            }
        }
    }
}