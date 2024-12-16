using System;
using Unione.Net.Exceptions;
using Unione.Net.Interfaces;

namespace Unione.Net;

public class ApiConfiguration : IApiConfiguration
{
    private string? _serverAddress { get; set; }
    private string? _apiUrl { get; set; }
    private string? _apiVersion { get; set; }
    private string? _apiKey { get; set; }
    private bool _enableLogging { get; set; }
    private int _timeout { get; set; }

    public string? ApiUrl => _apiUrl;
    public string? ApiVersion => _apiVersion;
    public string? ApiKey => _apiKey;
    public string? ServerAddress => _serverAddress;

    private ApiConfiguration()
    {
    }

    private ApiConfiguration(string serverAddress, string apiUrl, string apiVersion, string apiKey, bool enableLogging, int timeout)
    {
        _serverAddress = serverAddress;
        _apiUrl = apiUrl;
        _apiVersion = apiVersion;
        _apiKey = apiKey;
        _enableLogging = enableLogging;
        _timeout = timeout;
    }

    public static ApiConfiguration CreateNew(string serverAddress, string apiUrl, string apiVersion, string apiKey, bool enableLogging, int timeout)
    {
        if (string.IsNullOrEmpty(serverAddress))
            throw new EmptyApiConfigurationException("Server address cannot be empty!");
        if (string.IsNullOrEmpty(apiUrl))
            throw new EmptyApiConfigurationException("ApiUrl cannot be empty!");
        if (!apiUrl.Contains("/"))
            throw new EmptyApiConfigurationException("ApiUrl is invalid!");
        if (string.IsNullOrEmpty(apiVersion) && !apiUrl.Contains("/"))
            throw new EmptyApiConfigurationException("ApiVersion is invalid!");
        if (string.IsNullOrEmpty(apiKey))
            throw new EmptyApiConfigurationException("ApiKey cannot be empty!");

        if (!serverAddress.StartsWith("https://"))
            serverAddress = $"https://{serverAddress}";

        if (!serverAddress.EndsWith("/"))
            serverAddress = $"{serverAddress}/";

        if (!apiUrl.EndsWith("/"))
            apiUrl = $"{apiUrl}/";

        if (!apiVersion.EndsWith("/"))
            apiVersion = $"{apiVersion}/";

        return new ApiConfiguration(serverAddress, apiUrl, apiVersion, apiKey, enableLogging, timeout);
    }

    public string GetApiUrl()
    {
        return _serverAddress + _apiUrl + _apiVersion;
    }

    public string? GetApiKey()
    {
        return _apiKey;
    }

    public bool IsLoggingEnabled()
    {
        return _enableLogging;
    }

    public int GetTimeout()
    {
        return (int)TimeSpan.FromSeconds(_timeout).TotalMilliseconds;
    }
}