using GrainBroker.Controllers;
using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services;
using GrainBroker.Services.Interfaces;
using GrainBroker.Services.Models;
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
        private Mock<IRepository<Import>> _mockImportRepository;
        private Mock<ILogger<IImportService>> _mockLogger;
        private ImportService _importService;

        public ImportServiceTests()
        {
            _mockPurchaseOrderService = new();
            _mockImportRepository = new();
            _mockLogger = new();

            _importService = new(_mockPurchaseOrderService.Object, _mockImportRepository.Object, _mockLogger.Object);
        }
        [Fact]
        public void ServiceConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new ImportService(null, _mockImportRepository.Object, _mockLogger.Object));
            Assert.Throws<ArgumentNullException>(() => new ImportService(_mockPurchaseOrderService.Object, null, _mockLogger.Object));
            Assert.Throws<ArgumentNullException>(() => new ImportService(_mockPurchaseOrderService.Object, _mockImportRepository.Object, null));

        }

        [Fact]
        public async void ImportCompatibleCsv()
        {
            var fileName = "CompatibleDataset.csv";
        
            _mockImportRepository
                .Setup(x => x.Insert(It.IsAny<Import>()))
                .ReturnsAsync(new Import
                {
                    Id = Guid.NewGuid(),
                    FileName = fileName,
                    ImportDate = DateTime.Now
                });


            _mockPurchaseOrderService.Setup(x => x.Insert(It.IsAny<PurchaseOrderDTO>())).ReturnsAsync(new PurchaseOrder { 
            
            });
            using var reader = new StreamReader($"Services/ImportData/{fileName}");
            var result = await _importService.ImportPurchaseOrderCsv(reader, fileName);

            Assert.Equal(1, result);
        }

        [Fact]
        public async void ImportInCompatibleCsv()
        {
            var fileName = "InCompatibleDataset.csv";
            using var reader = new StreamReader($"Services/ImportData/{fileName}");
            var result = await _importService.ImportPurchaseOrderCsv(reader, fileName);

            Assert.Equal(0, result);
        }

        [Fact]
        public async void GetAll()
        {
            var expectedResult = new List<Import> {
            new Import
                {
                    Id = Guid.NewGuid(),
                    FileName = "TestFile.csv",
                    ImportDate = DateTime.Now
                }

            };

            _mockImportRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(expectedResult);
            var result = await _importService.GetAll();

            Assert.Single(result);
        }
    }
}
