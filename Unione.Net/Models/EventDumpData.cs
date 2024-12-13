using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class EventDumpFilter
{
    /// <summary>
    /// Job identifier returned earlier by email/send method.
    /// </summary>
    [JsonProperty("job_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? JobId { get; set; }

    /// <summary>
    /// Value from EventDumpStatus class
    /// </summary>
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    /// <summary>
    /// Value from DeliveryStatus class
    /// </summary>
    [JsonProperty("delivery_status", NullValueHandling = NullValueHandling.Ignore)]
    public string? DeliveryStatus { get; set; }

    /// <summary>
    /// Recipient email address
    /// </summary>
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    /// <summary>
    /// Sender email address
    /// </summary>
    [JsonProperty("email_from", NullValueHandling = NullValueHandling.Ignore)]
    public string? EmailFrom { get; set; }

    /// <summary>
    /// Recipient domain
    /// </summary>
    [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
    public string? Domain { get; set; }

    /// <summary>
    /// Campaign identifier, unsigned decimal integer or UUID up to 128-bit, passed in metadata with name “campaign_id” (name is configurable thru support).
    /// </summary>
    [JsonProperty("campaign_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? CampaignId { get; set; }
}

public class EventDumpRequest
{
    /// <summary>
    /// Date and time in YYYY-MM-DD hh:mm:ss format, specifying period start time. Data is stored for up to 32 days, depending on your tariff.
    /// </summary>
    [JsonProperty("start_time", NullValueHandling = NullValueHandling.Ignore)]
    public string? StartTime { get; set; }

    /// <summary>
    /// Date and time in YYYY-MM-DD hh:mm:ss format, specifying period end time (non-inclusive).
    /// </summary>
    [JsonProperty("end_time", NullValueHandling = NullValueHandling.Ignore)]
    public string? EndTime { get; set; }

    /// <summary>
    /// Maximum number of events returned (default is 50). If this value is over 100 000, several files will be created, each having 100 000 events maximum.
    /// </summary>
    [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
    public int? Limit { get; set; }

    /// <summary>
    /// For accounts with projects enabled, the value all_projects=true allows to fetch data for all existing projects.
    /// </summary>
    [JsonProperty("all_projects", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AllProjects { get; set; }

    /// <summary>
    /// An object with the properties of the event dump filter.
    /// </summary>
    [JsonProperty("filter", NullValueHandling = NullValueHandling.Ignore)]
    public EventDumpFilter? Filter { get; set; }

    /// <summary>
    /// Field delimiter, can be set to either ‘,’ or ‘;’, defaults to ‘,’.
    /// </summary>
    [JsonProperty("delimiter", NullValueHandling = NullValueHandling.Ignore)]
    public string? Delimiter { get; set; }

    /// <summary>
    /// File format, either “csv” (default) or “csv_gzip”.
    /// </summary>
    [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
    public string? Format { get; set; }
}

public class EventDumpStatus
{
    /// <summary>
    /// The message has been sent, but not delivered yet.
    /// </summary>
    public const string Sent = "sent";

    /// <summary>
    /// The message has been delivered. It can be changed to “opened”, “clicked”, “unsubscribed” or “spam”.
    /// </summary>
    public const string Delivered = "delivered";

    /// <summary>
    /// The message has been delivered and read. It can be changed to “clicked”, “unsubscribed” or “spam”.
    /// </summary>
    public const string Opened = "opened";

    /// <summary>
    /// The message has been delivered, read, and at least one of the links in email has been clicked. It can be changed to “unsubscribed” or “spam”.
    /// </summary>
    public const string Clicked = "clicked";

    /// <summary>
    /// The message has been delivered to the recipient and read, but then the recipient unsubscribed. The status is final.
    /// </summary>
    public const string Unsubscribed = "unsubscribed";

    /// <summary>
    /// Temporary delivery failure. UniOne will try to deliver the message during 48 hours. In case of success the status changes to “delivered”, in case of failure to deliver during 48 hours message status changes to “hard_bounced”.
    /// </summary>
    public const string Soft_bounced = "soft_bounced";

    /// <summary>
    /// Failed to deliver the message, there will be no delivery attempts. It’s a final status. There are a lot of possible reasons, you can use SMTP response from delivery_info.destination_response field or UniOne internal reason classification from delivery_info.delivery_status field to analyze the reason.
    /// </summary>
    public const string Hard_bounced = "hard_bounced";

    /// <summary>
    /// The message has been delivered, but it was reported as spam by the recipient. UniOne can receive and process the spam complaint from several domains, including msn.com, outlook.com, hotmail.com, live.com, ukr.net, yahoo.com, aol.com using FBL.
    /// </summary>
    public const string Spam = "spam";
}

public class DeliveryStatus
{
    /// <summary>
    /// Email address doesn’t exist
    /// </summary>
    public const string Err_user_unknown = "err_user_unknown";

    /// <summary>
    /// Email address isn’t used anymore
    /// </summary>
    public const string Err_user_inactive = "err_user_inactive";

    /// <summary>
    /// Email was temporary rejected, delivery will be retried later
    /// </summary>
    public const string Err_will_retry = "err_will_retry";

    /// <summary>
    /// Email address was active earlier, but now it’s deleted
    /// </summary>
    public const string Err_mailbox_discarded = "err_mailbox_discarded";

    /// <summary>
    /// Mailbox is full
    /// </summary>
    public const string Err_mailbox_full = "err_mailbox_full";

    /// <summary>
    /// Email was rejected as a spam
    /// </summary>
    public const string Err_spam_rejected = "err_spam_rejected";

    /// <summary>
    /// Email was rejected because sender IP or sender domain is found in a black list;
    /// </summary>
    public const string Err_blacklisted = "err_blacklisted";

    /// <summary>
    /// Email size is over limit, according to recipient’s server
    /// </summary>
    public const string Err_too_large = "err_too_large";

    /// <summary>
    /// Email was unsubscribed
    /// </summary>
    public const string Err_unsubscribed = "err_unsubscribed";

    /// <summary>
    /// Numerous delivery failures lead to mark this email address as permanently unavailable
    /// </summary>
    public const string Err_unreachable = "err_unreachable";

    /// <summary>
    /// Sending has been canceled because the email address is temporarily unavailable
    /// </summary>
    public const string Err_skip_letter = "err_skip_letter";

    /// <summary>
    /// The domain does not accept mail or does not exist
    /// </summary>
    public const string Err_domain_inactive = "err_domain_inactive";

    /// <summary>
    /// The domain does not accept mail due to incorrect settings on the recipient’s side, and the server’s response contains information about a cause that can be fixed (for example, an inoperative blacklist is used, etc.)
    /// </summary>
    public const string Err_destination_misconfigured = "err_destination_misconfigured";

    /// <summary>
    /// Delivery failed due to other reasons
    /// </summary>
    public const string Err_delivery_failed = "err_delivery_failed";

    /// <summary>
    /// Sending has been canceled because the campaign has been blocked as a spam
    /// </summary>
    public const string Err_spam_skipped = "err_spam_skipped";

    /// <summary>
    /// The message was not sent due to inconsistency of its parts or it has been lost because of the failure on our side. The sender needs to re-send the letter on his own, since the original has not been saved
    /// </summary>
    public const string Err_lost = "err_lost";
}

public class EventDumpList
{
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    [JsonProperty("event_dumps", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<EventDump>? EventDumps;
}

public class EventDump
{
    [JsonProperty("dump_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? DumpId { get; set; }

    [JsonProperty("dump_status", NullValueHandling = NullValueHandling.Ignore)]
    public string? DumpStatus { get; set; }

    [JsonProperty("files", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<DumpFiles>? Files { get; set; }
}

public class DumpFiles
{
    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public string? Url { get; set; }

    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public string? Size { get; set; }
}