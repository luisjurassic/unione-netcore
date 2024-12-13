namespace Unione.Net.Interfaces;

public interface IApiConfiguration
{
    string GetApiUrl();
    string? GetApiKey();
    bool IsLoggingEnabled();
    int GetTimeout();
}