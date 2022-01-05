using GrainBroker.Domain;
using GrainBroker.Domain.Models;
using GrainBroker.Domain.Repository;
using GrainBroker.Importer;
using GrainBroker.Services;
using GrainBroker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        IConfiguration configuration = hostContext.Configuration;
      
        services.AddDbContext<Context>(options =>
             options.UseSqlServer(hostContext.Configuration.GetConnectionString("GrainBrokerDb")), ServiceLifetime.Scoped);
        services.AddScoped<IRepository<PurchaseOrder>, PurchaseOrderRepository>();
        services.AddScoped<IRepository<Customer>, CustomerRepository>();
        services.AddScoped<IRepository<Supplier>, SupplierRepository>();
        services.AddScoped<IRepository<Location>, LocationRepository>();
        services.AddScoped<IImportService, ImportService>();
        services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ICustomerService, CustomerService>();
    })
    .Build();

await host.RunAsync();
