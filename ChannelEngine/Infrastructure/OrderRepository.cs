using System.Linq.Expressions;
using Domain;
using Domain.Configuration;
using Domain.Interface;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly RestClient _client;
    private readonly ConsoleConfiguration _consoleConfiguration;
    public OrderRepository(RestClient client, ConsoleConfiguration consoleConfiguration)
    {
        _client = client;
        _consoleConfiguration = consoleConfiguration;
    }

    public async Task<InProgress> Get(Expression<Func<InProgress, bool>> expression)
    {
        var (_, apiKey) = _consoleConfiguration;
        var request = new RestRequest($"/api/v2/orders?apikey={apiKey}&statuses=IN_PROGRESS", Method.Get);
        var response = await _client.GetAsync(request);
        
        if (!response.IsSuccessful)
        {
            //TODO create custom exception on domain
            throw new Exception();
        }
        
        return JsonConvert.DeserializeObject<InProgress>(response.Content);
    }
}