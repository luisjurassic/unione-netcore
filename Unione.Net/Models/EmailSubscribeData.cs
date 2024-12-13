using Newtonsoft.Json;

namespace Unione.Net.Models;

public class EmailSubscribeData
{
    /// <summary>
    /// Sender’s email.
    /// </summary>
    [JsonProperty("from_email", NullValueHandling = NullValueHandling.Ignore)]
    public string? FromEmail { get; set; }

    /// <summary>
    /// Sender’s name.
    /// </summary>
    [JsonProperty("from_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? FromName { get; set; }

    /// <summary>
    /// Recipient’s email.
    /// </summary>
    [JsonProperty("to_email", NullValueHandling = NullValueHandling.Ignore)]
    public string? ToEmail { get; set; }

    private EmailSubscribeData()
    {
    }

    private EmailSubscribeData(string from, string name, string to)
    {
        FromEmail = from;
        FromName = name;
        ToEmail = to;
    }

    public static EmailSubscribeData CreateNew(string from, string name, string to)
    {
        return new EmailSubscribeData(from, name, to);
    }
}