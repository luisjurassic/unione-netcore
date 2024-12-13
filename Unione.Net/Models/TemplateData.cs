using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class TemplateData
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// Value from EditorType class
    /// </summary>
    [JsonProperty("editor_type", NullValueHandling = NullValueHandling.Ignore)]
    public string? EditorType { get; set; }

    /// <summary>
    /// Value from TemplateEngine class
    /// </summary>
    [JsonProperty("template_engine", NullValueHandling = NullValueHandling.Ignore)]
    public string? TemplateEngine { get; set; }

    [JsonProperty("global_substitutions", NullValueHandling = NullValueHandling.Ignore)]
    public List<Dictionary<string, string>>? GlobalSubstitutions { get; set; }

    [JsonProperty("global_metadata", NullValueHandling = NullValueHandling.Ignore)]
    public List<Dictionary<string, string>>? GlobalMetadata { get; set; }

    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public Body? Body { get; set; }

    [JsonProperty("subject", NullValueHandling = NullValueHandling.Ignore)]
    public string? Subject { get; set; }

    [JsonProperty("from_email", NullValueHandling = NullValueHandling.Ignore)]
    public string? FromEmail { get; set; }

    [JsonProperty("from_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? FromName { get; set; }

    [JsonProperty("reply_to", NullValueHandling = NullValueHandling.Ignore)]
    public string? ReplyTo { get; set; }

    //[Range(0, 1)]
    [JsonProperty("track_links", NullValueHandling = NullValueHandling.Ignore)]
    public int TrackLinks { get; set; }

    //[Range(0, 1)]
    [JsonProperty("track_read", NullValueHandling = NullValueHandling.Ignore)]
    public int TrackRead { get; set; }

    [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
    public List<Dictionary<string, string>>? Headers { get; set; }

    [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
    public List<Attachment>? Attachments { get; set; }

    [JsonProperty("inline_attachments", NullValueHandling = NullValueHandling.Ignore)]
    public List<Attachment>? InlineAttachments { get; set; }

    [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
    public string? Created { get; set; }

    [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? UserId { get; set; }

    [JsonProperty("project_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? ProjectId { get; set; }

    [JsonProperty("project_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? ProjectName { get; set; }

    public string ToJson()
    {
        var jsonObject = new Dictionary<string, object>
        {
            ["template"] = new Dictionary<string, object>()
        };

        PropertyInfo[] properties = typeof(EmailMessageData).GetProperties();
        foreach (var property in properties)
        {
            var propertyName = GetJsonPropertyName(property);

            var propertyValue = property.GetValue(this);

            if (propertyValue != null) ((Dictionary<string, object?>)jsonObject["message"])[propertyName] = propertyValue;
        }

        return JsonConvert.SerializeObject(jsonObject, Formatting.Indented, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }

    private static string GetJsonPropertyName(PropertyInfo property)
    {
        var attribute = property.GetCustomAttribute<JsonPropertyAttribute>();
        return attribute?.PropertyName ?? property.Name;
    }
}

public class Body
{
    /// <summary>
    /// HTML part of the email body
    /// </summary>
    [JsonProperty("html", NullValueHandling = NullValueHandling.Ignore)]
    public string? Html { get; set; }

    /// <summary>
    /// Plaintext part of the email body.
    /// </summary>
    [JsonProperty("plaintext", NullValueHandling = NullValueHandling.Ignore)]
    public string? PlainText { get; set; }

    /// <summary>
    /// Optional AMP part of the email body.
    /// </summary>
    [JsonProperty("amp", NullValueHandling = NullValueHandling.Ignore)]
    public string? Amp { get; set; }
}

public class Attachment
{
    /// <summary>
    /// Attachment type, see MIME. If unsure, use “application/octet-stream”.
    /// </summary>
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string? Type { get; set; }

    /// <summary>
    /// Attachment name in the format: “name.extension”
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// File contents in base64. Maximum file size 7MB (9786710 bytes in base64).
    /// </summary>
    [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
    public string? Content { get; set; }
}

public class EditorType
{
    public const string Html = "html";
    public const string Visual = "visual";
}

public class TemplateEngine
{
    public const string None = "none";
    public const string Simple = "simple";
    public const string Velocity = "velocity";
}

public class InputData
{
    public string? Id { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }

    private InputData(string? id, int? limit, int? offset)
    {
        Id = id;
        Limit = limit;
        Offset = offset;
    }

    public static InputData CreateNew(string? id, int? limit, int? offset)
    {
        return new InputData(id, limit, offset);
    }
}

public class TemplateList
{
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? status;

    [JsonProperty("templates", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<TemplateData>? templates;
}