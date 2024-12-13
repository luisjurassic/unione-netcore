using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class UnsubscribedData
{
    /// <summary>
    /// Email address
    /// </summary>
    [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Date and time when a recipient unsubscribed, in UTC timezone and “YYYY-MM-DD hh:mm:ss” format.
    /// </summary>
    [JsonProperty("unsubscribed_on", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime UnsubscribedOn { get; set; }

    /// <summary>
    /// true if unsubscribed, false if not.
    /// </summary>
    [JsonProperty("is_unsubscribed", NullValueHandling = NullValueHandling.Ignore)]
    public bool IsUnsubscribed { get; set; }

    /// <summary>
    /// Human-readable error message in English.
    /// </summary>
    [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
    public string? Message { get; set; }

    private UnsubscribedData()
    {
    }

    private UnsubscribedData(string emailAddress, DateTime unsubscribedOn, bool isUnsubscribed, string message)
    {
        EmailAddress = emailAddress;
        UnsubscribedOn = unsubscribedOn;
        IsUnsubscribed = isUnsubscribed;
        Message = message;
    }

    public static UnsubscribedData CreateNew(string emailAddress, DateTime unsubscribedOn, bool isUnsubscribed, string message)
    {
        return new UnsubscribedData(emailAddress, unsubscribedOn, isUnsubscribed, message);
    }
}

public class UnsubscribedList
{
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? status { get; set; }

    [JsonProperty("unsubscribed", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<UnsubscribedData>? Unsubscribed { get; set; }
}