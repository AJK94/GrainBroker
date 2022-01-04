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
             options.UseSqlServer(hostContext.Configuration.GetConnectionString("GrainBrokerDb")), ServiceLifetime.Singleton);
        services.AddSingleton<IRepository<PurchaseOrder>, PurchaseOrderRepository>();
        services.AddSingleton<IRepository<Customer>, CustomerRepository>();
        services.AddSingleton<IRepository<Supplier>, SupplierRepository>();
        services.AddSingleton<IRepository<Location>, LocationRepository>();
        services.AddSingleton<IPurchaseOrderService, PurchaseOrderService>();
        services.AddSingleton<ILocationService, LocationService>();
        services.AddSingleton<ISupplierService, SupplierService>();
        services.AddSingleton<ICustomerService, CustomerService>();
    })
    .Build();

await host.RunAsync();
