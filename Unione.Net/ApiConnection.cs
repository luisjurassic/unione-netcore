using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Unione.Net.Interfaces;

namespace Unione.Net;

public class ApiConnection(IApiConfiguration apiConfiguration) : IApiConnection
{
    private readonly bool _enableLogging = apiConfiguration.IsLoggingEnabled();

    public async Task<(string, string)> SendMessageAsync(string command, object request)
    {
        string? apiResponse = string.Empty;
        string? responseBody = string.Empty;

        using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(apiConfiguration.GetTimeout()))
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(apiConfiguration.GetApiUrl());
            client.DefaultRequestHeaders.Add("X-API-KEY", apiConfiguration.GetApiKey());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string? requestBody = requestBody = !request.ToString()!.Contains("{") ? JsonSerializer.Serialize(request) : request.ToString();

            if (requestBody != null)
            {
                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage? response = await client.PostAsync(command, content, cancellationTokenSource.Token);

                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        apiResponse = "Request cancelled due to timeout.";
                    }
                    else
                    {
                        apiResponse = response.StatusCode.ToString();
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (TaskCanceledException)
                {
                    apiResponse = "Request cancelled due to timeout.";
                }
                catch (Exception ex)
                {
                    apiResponse = ex.Message;
                }
            }

            return (apiResponse, responseBody);
        }
    }

    public bool IsLoggingEnabled()
    {
        return _enableLogging;
    }
}