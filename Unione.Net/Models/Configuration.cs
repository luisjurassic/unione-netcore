namespace Unione.Net.Models;

public class Configuration
{
    public string ServerAddress { get; set; }
    public string ApiUrl { get; set; }
    public string ApiVersion { get; set; }
    public string ApiKey { get; set; }
    public int ServerTimeout { get; set; }
    public bool EnableLogging { get; set; }
}