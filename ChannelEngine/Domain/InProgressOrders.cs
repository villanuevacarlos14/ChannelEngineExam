using Domain.Interface;
using Newtonsoft.Json;

namespace Domain;

[Serializable]
public class InProgressOrders
{
    public InProgressOrders()
    {
        Content = new List<Order>();
    }

    [JsonProperty]
    public virtual IEnumerable<Order> Content { get; private set; }

    public static Task<InProgressOrders> CreateAsync(IOrderRepository repository)
    {
        var ret = new InProgressOrders();
        return ret.InitializeAsync(repository);
    }

    private async Task<InProgressOrders> InitializeAsync(IOrderRepository repository)
    {
        this.Content = (await repository.GetInProgress()).Content;
        return this;
    }

    public IEnumerable<Line> GetTop5SoldProducts()
    {
        var resp = this.Content
            .SelectMany(x => x.Lines)
            .GroupBy(x => x.Description)
            .Select(x => new
            {
                item = x,
                totalQuantity = x.Sum(y => y.Quantity)
            })
            .OrderByDescending(x => x.totalQuantity)
            .Take(5)
            .Select(x =>
            {
                var first = x.item.First();
                return new Line(first.MerchantProductNo,first.Gtin,first.Description,x.totalQuantity);
            });

        return resp;
    }
}