using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class WebhookData
{
    /// <summary>
    /// Webhook unique identifier.
    /// </summary>
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public int? Id { get; set; }

    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public string? Url { get; set; }

    /// <summary>
    /// Webhook status, “active” by default. “disabled” means that webhook has been disabled by the user, “stopped” means that webhook has been stopped by the system after 24h of failed calls (with minimum of 10 distinct events). Value from WebhookStatus class
    /// </summary>
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    /// <summary>
    /// Notification format. “json_post”(default) or “json_post_gzip”. Value from EventFormat class
    /// </summary>
    [JsonProperty("event_format", NullValueHandling = NullValueHandling.Ignore)]
    public string? EventFormat { get; set; }

    /// <summary>
    /// Shall detailed delivery info be returned(1) or not(0). When it’s 1, the information about SMTP response and internal delivery status is returned for “hard_bounced” and “soft_bounced” email statuses; the user agent is returned for “opened” and “clicked” statuses and URL for “clicked” status.
    /// </summary>
    [JsonProperty("delivery_info", NullValueHandling = NullValueHandling.Ignore)]
    public int? DeliveryInfo { get; set; }

    /// <summary>
    /// 0=several event notifications can be reported in single webhook call, 1=only single event can be reported in one call (not recommended).
    /// </summary>
    [JsonProperty("single_event", NullValueHandling = NullValueHandling.Ignore)]
    public int? SingleEvent { get; set; }

    /// <summary>
    /// Maximum quantity of permitted parallel queries to your server. The more your server can handle - the better.
    /// </summary>
    [JsonProperty("max_parallel", NullValueHandling = NullValueHandling.Ignore)]
    public int? MaxParallel { get; set; }

    /// <summary>
    /// Object containing events to notify of.
    /// </summary>
    [JsonProperty("events", NullValueHandling = NullValueHandling.Ignore)]
    public Event? Events { get; set; }

    /// <summary>
    /// Webhook properties last update date and time in UTC timezone in “YYYY-MM-DD hh:mm:ss” format.
    /// </summary>
    [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? UpdatedAt { get; set; }
}

public class WebhookStatus
{
    public const string Active = "active";
    public const string Disabled = "disabled";
}

public class EventFormat
{
    public const string Json_Post = "json_post";
    public const string Json_Post_Gzip = "json_post_gzip";
}

public class Event
{
    /// <summary>
    /// If present then spam block events will be reported. Should contain single array element with “*” string.
    /// </summary>
    [JsonProperty("spam_block", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Spam_block { get; set; }

    /// <summary>
    /// If present then email status change events will be reported. Contains names of statuses to notify of. List of values from EmailStatus class
    /// </summary>
    [JsonProperty("email_status", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Email_status { get; set; }
}

public class EmailStatus
{
    public const string Delivered = "delivered";
    public const string Opened = "opened";
    public const string Clicked = "clicked";
    public const string Unsubscribed = "unsubscribed";
    public const string Subscribed = "subscribed";
    public const string Soft_bounced = "soft_bounced";
    public const string Hard_bounced = "hard_bounced";
    public const string Spam = "spam";
}