using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class DeliveryInfo
{
    /// <summary>
    /// UniOne internal detailed delivery status. String value from DeliveryStatus class
    /// </summary>
    [JsonProperty("delivery_status", NullValueHandling = NullValueHandling.Ignore)]
    public string? DeliveryStatus { get; set; }

    /// <summary>
    /// SMTP response.
    /// </summary>
    [JsonProperty("destination_response", NullValueHandling = NullValueHandling.Ignore)]
    public string? DestinationResponse { get; set; }

    /// <summary>
    /// User agent of recipient. Present only if detected for “clicked” and “opened” statuses.
    /// </summary>
    [JsonProperty("user_agent", NullValueHandling = NullValueHandling.Ignore)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Recipient’s IP address. Present only if detected for “clicked” and “opened” statuses.
    /// </summary>
    [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
    public string? Ip { get; set; }
}

public class EventData
{
    /// <summary>
    /// Job identifier returned earlier by email/send method. Property exists only if event_name=“transactional_email_status”.
    /// </summary>
    [JsonProperty("job_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? JobId { get; set; }

    /// <summary>
    /// Metadata passed in email/send method in recipients.metadata or global_metadata properties. This property exists only if event_name=“transactional_email_status”.
    /// </summary>
    [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Recipient’s email. This property exists only if event_name=“transactional_email_status”.
    /// </summary>
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    /// <summary>
    /// Email delivery status. This property exists only if event_name=“transactional_email_status”. String value from EventDumpStatus class
    /// </summary>
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    /// <summary>
    /// Event date & time in UTC time zone in “YYYY-MM-DD hh:mm:ss” format. This property exists only if event_name=“transactional_email_status”.
    /// </summary>
    [JsonProperty("event_time", NullValueHandling = NullValueHandling.Ignore)]
    public string? EventTime { get; set; }

    /// <summary>
    /// URL for “opened” and “clicked” statuses. This property exists only if event_name=“transactional_email_status”.
    /// </summary>
    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public string? Url { get; set; }

    /// <summary>
    /// Object with detailed delivery info.Event date & time in UTC time zone in “YYYY-MM-DD hh:mm:ss” format. This property exists only if webhook has delivery_info proeprty set to 1 and event_name=“transactional_email_status”.
    /// </summary>
    [JsonProperty("delivery_info", NullValueHandling = NullValueHandling.Ignore)]
    public DeliveryInfo? DeliveryInfo { get; set; }

    /// <summary>
    /// Spam block date & time in UTC time zone in “YYYY-MM-DD hh:mm:ss” format. This property exists only if event_name=“transactional_spam_block”.
    /// </summary>
    [JsonProperty("block_time", NullValueHandling = NullValueHandling.Ignore)]
    public string? BlockTime { get; set; }

    /// <summary>
    /// Spam block type, either single or multiple sending SMTP. For single sending SMTP block in common pool UniOne retries several other SMTPs. This property exists only if event_name=“transactional_spam_block”.
    /// </summary>
    [JsonProperty("block_type", NullValueHandling = NullValueHandling.Ignore)]
    public string? BlockType { get; set; }

    /// <summary>
    /// Domain that blocked sending. This property exists only if event_name=“transactional_spam_block”.
    /// </summary>
    [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
    public string? Domain { get; set; }

    /// <summary>
    /// Number of sending SMTPs blocked. This property exists only if event_name=“transactional_spam_block”.
    /// </summary>
    [JsonProperty("SMTP_blocks_count", NullValueHandling = NullValueHandling.Ignore)]
    public int? SMTPBlocksCount { get; set; }

    /// <summary>
    /// Whether it’s a block or unblock event. This property exists only if event_name=“transactional_spam_block”.
    /// </summary>
    [JsonProperty("domain_status", NullValueHandling = NullValueHandling.Ignore)]
    public string? DomainStatus { get; set; }
}

public class WebhookEvent
{
    /// <summary>
    /// Type of event data contained in event_data field, either “transactional_email_status” or “transactional_spam_block”.
    /// </summary>
    [JsonProperty("event_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? EventName { get; set; }

    /// <summary>
    /// Object with different event properties depending on “event_name”. Below you can see all the properties, “transactional_email_status”-related first and then “transactional_spam_block”-related.
    /// </summary>
    [JsonProperty("event_data", NullValueHandling = NullValueHandling.Ignore)]
    public EventData? EventData { get; set; }
}

public class EventsByUser
{
    /// <summary>
    /// Unique user identifier.
    /// </summary>
    [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
    public int? UserId { get; set; }

    /// <summary>
    /// Project identifier, present only if webhook was registered for the project using project API key.
    /// </summary>
    [JsonProperty("project_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Project name, present only if webhook was registered for the project using project API key.
    /// </summary>
    [JsonProperty("project_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? ProjectName { get; set; }

    /// <summary>
    /// Array of events reported by webhook.
    /// </summary>
    [JsonProperty("events", NullValueHandling = NullValueHandling.Ignore)]
    public List<WebhookEvent>? Events { get; set; }
}

public class CallbackData
{
    /// <summary>
    /// MD5-hash of the message body, in which the value “auth” is replaced by api key of the user/project; with this field the recipient of the notification can both authenticate and verify the notification integrity
    /// </summary>
    [JsonProperty("auth1", NullValueHandling = NullValueHandling.Ignore)]
    public string? Auth { get; set; }

    /// <summary>
    /// Array with only one element, containing events of a user/project.
    /// </summary>
    [JsonProperty("events_by_user", NullValueHandling = NullValueHandling.Ignore)]
    public List<EventsByUser>? EventsByUser { get; set; }
}