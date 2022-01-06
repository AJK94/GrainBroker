using GrainBroker.Controllers;
using GrainBroker.Domain;
using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GrainBroker.Tests.Domain
{
    public class ImportRepositoryTests
    {
        Context _context;
        ImportRepository _importRepository;
        InMemoryContextSeeder _contextSeeder;

        public ImportRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
                 .EnableSensitiveDataLogging()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;
            _context = new Context(options);

            _importRepository = new ImportRepository(_context);

            _contextSeeder = new InMemoryContextSeeder(_context);

            _contextSeeder.SeedContext();

        }

        [Fact]
        public void RepositoryConstructorExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new ImportRepository(null));
        }

        [Fact]
        public async void GetAll()
        {
            var imports = await _importRepository.GetAll();
            Assert.Single(imports);
            var import = imports.FirstOrDefault();
            Assert.NotNull(import);
            Assert.Equal("TestFile.csv", import.FileName);
            Assert.Single(import.PurchaseOrders);

        }

        [Fact]
        public async void GetExistingById()
        {
            var import = await _importRepository.GetById(new Guid("3abd1602-b0bf-495a-b833-e158ca28ff13"));

            Assert.Equal("TestFile.csv", import?.FileName);
        }

        [Fact]
        public async void GetNonExistingById()
        {
            var import = await _importRepository.GetById(Guid.NewGuid());

            Assert.Null(import);
        }

        [Fact]
        public async void Insert()
        {
            _importRepository.Insert(new Import
            {
                Id = Guid.NewGuid(),
                FileName = "New.csv",
                ImportDate = DateTime.Now
            }); 
            var imports = await _importRepository.GetAll();
            Assert.Equal(2, imports.Count());
        }
    }
}