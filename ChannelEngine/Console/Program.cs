// See https://aka.ms/new-console-template for more information

using Domain.Configuration;
using Domain.Interface;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var config = hostContext.Configuration;
        services.AddSingleton(new ConsoleConfiguration(config["ChannelEngineConf:ApiUrl"],
            config["ChannelEngineConf:ApiKey"]));
        services.AddTransient(x => new RestClient("https://api-dev.channelengine.net"));
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
    })
    .Build();


using IServiceScope serviceScope = host.Services.CreateScope();
var provider = serviceScope.ServiceProvider;
var orderRepository = provider.GetRequiredService<IOrderRepository>();
var productRepository = provider.GetRequiredService<IProductRepository>();

var data = await orderRepository.Get(x => true);
var top5 = data.GetTop5SoldProducts();
foreach (var line in top5)
{
    Console.WriteLine($"MerchantProductNo: {line.MerchantProductNo} - GTIN: {line.Gtin} - Description: {line.Description} - Quantity: {line.Quantity}");
}

var productUpdated = false;

while (!productUpdated)
{
    var merchantProductId = Console.ReadLine();
    if (top5.Any(x => x.MerchantProductNo == merchantProductId))
    {
        var line = top5.FirstOrDefault(x => x.MerchantProductNo == merchantProductId);
        productUpdated = await line.Product.UpdateProductStock(25, productRepository);
        
        Console.WriteLine("Product stock updated to 25 Successfully!");
    }
}



await host.RunAsync();

