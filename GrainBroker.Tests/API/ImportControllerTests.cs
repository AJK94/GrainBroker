using GrainBroker.Controllers;
using GrainBroker.Domain.Models;
using GrainBroker.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GrainBroker.Tests.API
{
    public class ImportControllerTests
    {
        private Mock<ILogger<ImportController>> _mockLogger;
        private Mock<IImportService> _mockImportService;
        private ImportController _importController;

        public ImportControllerTests()
        {
            _mockLogger = new();
            _mockImportService = new();
            _importController = new(_mockLogger.Object, _mockImportService.Object);

        }
        [Fact]
        public void ControllerConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new ImportController(null, _mockImportService.Object));
            Assert.Throws<ArgumentNullException>(() => new ImportController(_mockLogger.Object, null));
        }

        [Fact]
        public async void GetPurchaseOrders_Handles_Exception()
        {
            _mockImportService
                 .Setup(pos => pos.GetAll())
                 .Throws(new Exception());

            var result = await _importController.GetImports();

            Assert.NotNull(result);
            Assert.IsType<StatusCodeResult>(result);
            var actualResult = (StatusCodeResult)result;
            Assert.Equal(StatusCodes.Status500InternalServerError, actualResult.StatusCode);
        }

        [Fact]
        public async void GetPurchaseOrders_Returns_Values()
        {
            var expectedResult = new List<Import>{
                new Import {
                    Id = Guid.NewGuid(),
                    FileName = "TestFile.csv",
                    ImportDate = DateTime.Now,
                }
            };

            _mockImportService
                 .Setup(pos => pos.GetAll())
                 .ReturnsAsync(expectedResult);

            var result = await _importController.GetImports();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.Equal(expectedResult, actualResult.Value);
        }
    }
}
