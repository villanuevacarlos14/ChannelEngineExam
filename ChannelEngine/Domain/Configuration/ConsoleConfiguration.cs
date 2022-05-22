namespace Domain.Configuration;

public class ConsoleConfiguration
{
    public ConsoleConfiguration(string apiUrl, string apiKey)
    {
        ChannelEngineConf = new ChannelEngineConfiguration(apiUrl, apiKey);
    }

    public ChannelEngineConfiguration ChannelEngineConf { get; private set; }

    public void Deconstruct(out string apiUrl, out string apiKey)
    {
        apiUrl = this.ChannelEngineConf.ApiUrl;
        apiKey = this.ChannelEngineConf.ApiKey;
    }
}

public class ChannelEngineConfiguration
{
    public ChannelEngineConfiguration(string apiUrl, string apiKey)
    {
        ApiUrl = apiUrl;
        ApiKey = apiKey;
    }

    public string ApiUrl { get; private set; }
    public string ApiKey { get; private set; }
}