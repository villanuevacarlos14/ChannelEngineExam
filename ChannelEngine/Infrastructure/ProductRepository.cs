using Domain;
using Domain.Configuration;
using Domain.Interface;
using RestSharp;

namespace Infrastructure;

public class ProductRepository: IProductRepository
{
    private readonly RestClient _client;
    private readonly ConsoleConfiguration _consoleConfiguration;
    public ProductRepository(RestClient client, ConsoleConfiguration consoleConfiguration)
    {
        _client = client;
        _consoleConfiguration = consoleConfiguration;
    }
    
    public async Task<bool> UpdateStock(Product product, int stock)
    {
        var (_, apiKey) = _consoleConfiguration;
        var request = new RestRequest($"/api/v2/products/{product.MerchantProductNo}?apikey={apiKey}", Method.Patch);
       
        var body = new List<object>()
        {
            new {
                op = "replace",
                path = "Stock",
                value = stock
            }
        };
        request.AddBody(body);
        var response = await _client.PatchAsync(request);
        
        if (!response.IsSuccessful)
        {
            //TODO create custom exception on domain
            throw new Exception();
        }

        return response.IsSuccessful;
    }
}