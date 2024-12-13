using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class EmailResponseData
{
    /// <summary>
    /// “success” string
    /// </summary>
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    /// <summary>
    /// Job identifier, might be useful for investigating errors.
    /// </summary>
    [JsonProperty("job_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? JobId { get; set; }

    /// <summary>
    /// Array of recipients emails successfully accepted for sending.
    /// </summary>
    [JsonProperty("emails", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Emails { get; set; }

    /// <summary>
    /// Object with emails rejected for sending as property names and their statuses as property values, e.g.: {“email1@gmail.com”: “temporary_unavailable”}. Possible statuses:
    ///    unsubscribed - specified email is unsubscribed;
    ///    invalid - the email does not exist or has been entered incorrectly;
    ///    duplicate - the email already exists in the request, email duplicating is prevented;
    ///    temporary_unavailable - the email address is unavailable. This means that over the next three days sending to this address will return an error. Email may be temporarily unavailable due to several reasons, e.g.:
    ///    a previous email has been rejected by the recipient’s server for spam;
    ///    the recipient’s mailbox is full or is not used;
    ///    recipient’s domain does not accept mail;
    ///    sending server was rejected due to blacklisting;
    ///    permanent_unavailable - the email address is permanently unavailable or unsubscribed globally.
    ///    complained - the recipient reported spam in the previous emails;
    ///    blocked - sending to the email is prohibited by administration of UniOne.
    ///    We may added new statuses in the future.
    /// </summary>
    [JsonProperty("failed_emails", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? FailedEmails { get; set; }
}