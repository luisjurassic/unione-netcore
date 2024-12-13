using AutoMapper.Configuration.Annotations;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class ErrorData
{
    public string? Status { get; set; }
    public ErrorDetailsData? Details { get; set; }
}

public class ErrorDetailsData
{
    /// <summary>
    /// “error” string
    /// </summary>
    [SourceMember("status")]
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    /// <summary>
    /// Human-readable error message in English.
    /// </summary>
    [SourceMember("message")]
    [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
    public string? Message { get; set; }

    /// <summary>
    /// API Error code - https://docs.unione.io/en/web-api-ref#api-errors
    /// </summary>
    [SourceMember("code")]
    [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
    public int Code { get; set; }

    public ErrorDetailsData()
    {
    }

    private ErrorDetailsData(string status, string message, int code)
    {
        Status = status;
        Message = message;
        Code = code;
    }

    public static ErrorDetailsData CreateNew(string status, string message, int errorCode)
    {
        return new ErrorDetailsData(status, message, errorCode);
    }
}