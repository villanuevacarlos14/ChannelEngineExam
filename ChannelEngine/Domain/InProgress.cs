using Newtonsoft.Json;

namespace Domain;

[Serializable]
public class InProgress
{
    public InProgress(IEnumerable<Order> content)
    {
        this.Content = content;
    }
    [JsonProperty]
    public IEnumerable<Order> Content { get; private set; }

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
                var i = x.item.First();
                i.ModifyQuantiy(x.totalQuantity);
                return i;
            });

        return resp;
    }
}