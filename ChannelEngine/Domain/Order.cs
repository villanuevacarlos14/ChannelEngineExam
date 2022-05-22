using Newtonsoft.Json;

namespace Domain;

[Serializable]
public class Order
{
    public Order(string status, IEnumerable<Line> lines)
    {
        Status = status;
        Lines = lines;
    }

    [JsonProperty]
    public string Status { get; private set; }
    
    [JsonProperty]
    public IEnumerable<Line> Lines { get; private set; }

}