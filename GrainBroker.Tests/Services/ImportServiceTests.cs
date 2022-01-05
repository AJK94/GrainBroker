using GrainBroker.Controllers;
using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services;
using GrainBroker.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GrainBroker.Tests.Services
{
    public class ImportServiceTests
    {
        private Mock<IPurchaseOrderService> _mockPurchaseOrderService;
        private Mock<ILogger<IImportService>> _mockLogger;
        private ImportService _importService;

        public ImportServiceTests()
        {
            _mockPurchaseOrderService = new();
            _mockLogger = new();

            _importService = new(_mockPurchaseOrderService.Object, _mockLogger.Object);
        }
        [Fact]
        public void ServiceConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new ImportService(null, _mockLogger.Object));
            Assert.Throws<ArgumentNullException>(() => new ImportService(_mockPurchaseOrderService.Object, null));

        }

        [Fact]
        public async void ImportCompatibleCsv()
        {
            using var reader = new StreamReader("Services/ImportData/CompatibleDataset.csv");
            var result = await _importService.ImportPurchaseOrderCsv(reader);

            Assert.Equal(1, result);
        }

        [Fact]
        public async void ImportInCompatibleCsv()
        {
            using var reader = new StreamReader("Services/ImportData/InCompatibleDataset.csv");
            var result = await _importService.ImportPurchaseOrderCsv(reader);

            Assert.Equal(0, result);
        }
    }
}
