namespace Unione.Net.Models;

public class Configuration
{
    public string ServerAddress { get; set; } = string.Empty;
    public string ApiUrl => "en/transactional/api/";
    public string ApiVersion => "v1";
    public string ApiKey { get; set; } = string.Empty;
    public int ServerTimeout { get; set; } = 30;
    public bool EnableLogging { get; set; }
}