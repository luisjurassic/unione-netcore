using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class Options
{
    /// <summary>
    /// Date and time in “YYYY-MM-DD hh:mm:ss” format in the UTC timezone. Allows schedule sending up to 24 hours in advance.
    /// </summary>
    [JsonProperty("send_at", NullValueHandling = NullValueHandling.Ignore)]
    public string? SendAt { get; set; }

    /// <summary>
    /// Custom unsubscribe link. Read more here - https://docs.unione.io/en/unsubscribe-link
    /// </summary>
    [JsonProperty("unsubscribe_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? UnsubscribeUrl { get; set; }

    /// <summary>
    /// Backend-domain identifier to send message with. If custom_backend_id is not set, default one for your account/project will be used. You can pay for one or more dedicated IPs and use this option.
    /// </summary>
    [JsonProperty("custom_backend_id", NullValueHandling = NullValueHandling.Ignore)]
    public int? CustomBackendId { get; set; }

    /// <summary>
    /// SMTP pool identifier to send message with. If smtp_pool_id is not set, default one for your account/project will be used. You can pay for one or more dedicated IPs and use this option to choose sending using dedicated IP or common pool.
    /// </summary>
    [JsonProperty("smtp_pool_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? SmtpPoolId { get; set; }
}

public class EmailMessageData
{
    /// <summary>
    /// Contains recipient emails, substitutions(merge tags) and metadata.
    /// </summary>
    [JsonProperty("recipients", NullValueHandling = NullValueHandling.Ignore)]
    public List<EmailRecipientData>? Recipients { get; set; }

    /// <summary>
    /// Optional identifier of the template that had been created by template/set method. If template_id is passed then fields from the template are used instead of missed email/send parameters. E.g. if “body” parameter in email/send is omitted, “body” from template will be used. If “subject” in email/send is omitted, “subject” from template will be used.
    /// </summary>
    [JsonProperty("template_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? TemplateId { get; set; }

    /// <summary>
    /// An array of strings. The maximum string length is 50 characters. You can include up to 4 strings which must be unique. No more than 10 000 tags are allowed per project; if you try to add more, email/send will return an error. You may use tags to categorize emails by your own criteria, and they will be passed along by event-dump methods.
    /// </summary>
    [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Whether to skip or not appending default unsubscribe footer. 1=skip, 0=append, default is 0. You should ask support to approve
    /// </summary>
    [JsonProperty("skip_unsubscribe", NullValueHandling = NullValueHandling.Ignore)]
    public int? SkipUnsubscribe { get; set; }

    /// <summary>
    /// The language of the unsubscribe footer and unsubscribe page. Languages supported are: “be”, “de”, “en”, “es”, “fr”, “it”, “pl”, “pt”, “ru”, “ua”.
    /// </summary>
    [JsonProperty("global_language", NullValueHandling = NullValueHandling.Ignore)]
    public string? GlobalLanguage { get; set; }

    /// <summary>
    /// The template engine for handling the substitutions(merge tags), “simple”, “velocity” or “none”. Default value is “simple”. “none” is only available for emails with “track_links” and “track_read” equal to 0 and the unsubscription link turned off.
    /// </summary>
    [JsonProperty("template_engine", NullValueHandling = NullValueHandling.Ignore)]
    public string? TemplateEngine { get; set; }

    /// <summary>
    /// Object for passing the substitutions(merge tags) common for all recipients - e.g., company name. If the substitution names are duplicated in object “substitutions”, the values of the variables will be taken from the object “substitutions”. The substitutions can be used in the following parameters:
    /// </summary>
    [JsonProperty("global_substitutions", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? GlobalSubstitutions { get; set; }

    /// <summary>
    /// Object for passing the metadata common for all the recipients, such as “key”: “value”. Max key quantity: 10. Max key length: 64 symbols. Max value length: 1024 symbols. The metadata is returned by webhook.
    /// </summary>
    [JsonProperty("global_metadata", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? GlobalMetadata { get; set; }

    /// <summary>
    /// Contains HTML/plaintext/AMP parts of the email. Either html or plaintext part is required.
    /// </summary>
    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public Body? Body { get; set; }

    /// <summary>
    /// Email subject.
    /// </summary>
    [JsonProperty("subject", NullValueHandling = NullValueHandling.Ignore)]
    public string? Subject { get; set; }

    /// <summary>
    /// Sender’s email. Required only if template_id parameter is empty.
    /// </summary>
    [JsonProperty("from_email", NullValueHandling = NullValueHandling.Ignore)]
    public string? FromEmail { get; set; }

    /// <summary>
    /// Sender’s name.
    /// </summary>
    [JsonProperty("from_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? FromName { get; set; }

    /// <summary>
    /// Optional Reply-To email (in case it’s different to sender’s email)
    /// </summary>
    [JsonProperty("reply_to", NullValueHandling = NullValueHandling.Ignore)]
    public string? ReplyTo { get; set; }

    /// <summary>
    /// 1=click tracking is on (default), 0=click tracking is off
    /// </summary>
    [JsonProperty("track_links", NullValueHandling = NullValueHandling.Ignore)]
    public int? TrackLinks { get; set; }

    /// <summary>
    /// 1=read tracking is on (default), 0=read tracking is off.
    /// </summary>
    [JsonProperty("track_read", NullValueHandling = NullValueHandling.Ignore)]
    public int? TrackRead { get; set; }

    /// <summary>
    /// (allowed values 0 or 1, default 0) If set to 1, the global unavailability list will be ignored. Even if the address was found to be unreachable while sending other UniOne users’ emails, or its owner has issued complaints, the message will still be sent. The setting may be ignored for certain addresses.
    /// </summary>
    [JsonProperty("bypass_global", NullValueHandling = NullValueHandling.Ignore)]
    public int? BypassGlobal { get; set; }

    /// <summary>
    /// (allowed values 0 or 1, default 0) If set to 1, the current user’s or project’s unavailability list will be ignored. Works only if bypass_global is set to 1.
    /// </summary>
    [JsonProperty("bypass_unavailable", NullValueHandling = NullValueHandling.Ignore)]
    public int? BypassUnavailable { get; set; }

    /// <summary>
    /// (allowed values 0 or 1, default 0) If set to 1, the current list of unsubscribed addresses for this account or project will be ignored. Works only if bypass_global is set to 1. The setting is available only for users that have been granted the right to omit the unsubscribe link (to request, please contact support).
    /// </summary>
    [JsonProperty("bypass_unsubscribed", NullValueHandling = NullValueHandling.Ignore)]
    public int? BypassUnsubscribed { get; set; }

    /// <summary>
    /// (allowed values 0 or 1, default 0) If set to 1, the user’s or project’s complaint list will be ignored. Works only if bypass_global is set to 1. The setting is available only for users that have been granted the right to omit the unsubscribe link (to request, please contact support).
    /// </summary>
    [JsonProperty("bypass_complained", NullValueHandling = NullValueHandling.Ignore)]
    public int? BypassComplained { get; set; }

    /// <summary>
    /// Contains email headers, maximum 50. Only headers with “X-” name prefix are accepted, all other are ignored, for example X-UNIONE-Global-Language, X-UNIONE-Template-Engine. If our support have approved omitting standard unsubscription block for you, you can also pass List-Unsubscribe, List-Subscribe, List-Help, List-Owner, List-Archive, In-Reply-To and References headers.
    /// X-UNIONE-Global-Language - The language of the unsubscribe footer and unsubscribe page. Languages supported are: “be”, “de”, “en”, “es”, “fr”, “it”, “pl”, “pt”, “ru”, “ua”.
    /// X-UNIONE-Template-Engine - The template engine for handling the substitutions(merge tags), “simple” or “velocity”. Default value is “simple”. Has a priority over the value allowed in the “template_engine” parameter.
    /// </summary>
    [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Optional array of attachments
    /// </summary>
    [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
    public List<Attachment>? Attachments { get; set; }

    /// <summary>
    /// Optional array of inline attachments, e.g. for including images in email instead of loading them from external URL.
    /// </summary>
    [JsonProperty("inline_attachments", NullValueHandling = NullValueHandling.Ignore)]
    public List<Attachment>? InlineAttachments { get; set; }

    /// <summary>
    /// Additional message options.
    /// </summary>
    [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
    public Options? Options { get; set; }

    public string ToJson()
    {
        Dictionary<string, object> jsonObject = new Dictionary<string, object>
        {
            ["message"] = new Dictionary<string, object>()
        };

        PropertyInfo[] properties = typeof(EmailMessageData).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            string propertyName = GetJsonPropertyName(property);

            object? propertyValue = property.GetValue(this);

            if (propertyValue != null) ((Dictionary<string, object>)jsonObject["message"])[propertyName] = propertyValue;
        }

        return JsonConvert.SerializeObject(jsonObject, Formatting.Indented, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }

    private static string GetJsonPropertyName(PropertyInfo property)
    {
        JsonPropertyAttribute? attribute = property.GetCustomAttribute<JsonPropertyAttribute>();
        return attribute?.PropertyName ?? property.Name;
    }
}