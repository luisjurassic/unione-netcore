using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class ProjectData
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id { get; set; }

    /// <summary>
    /// Project name, unique for user account.
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// API key of the project. You can use it instead of the user API key parameter in all methods except project/* methods.
    /// </summary>
    [JsonProperty("api_key", NullValueHandling = NullValueHandling.Ignore)]
    public string? ApiKey { get; set; }

    /// <summary>
    /// ISO-3166 alpha-2 country code. If set, UniOne treats project personal data according to country laws, e.g. GDPR for european countries.
    /// </summary>
    [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
    public string? Country { get; set; }

    [JsonProperty("req_time", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime Reg_time { get; set; }

    /// <summary>
    /// Whether email sending is enabled for this project or not.
    /// </summary>
    [JsonProperty("send_enabled", NullValueHandling = NullValueHandling.Ignore)]
    public bool Send_enabled { get; set; }

    /// <summary>
    /// If it’s false then UniOne adds default unsubscribe footer to each email sent with API key of the project. If it’s true then appending default unsubscribe footer for this project is avoided and sending with custom unsubscribe url or even without unsubscribe url is permitted for the project.
    /// Please note that custom_unsubscribe_url_enabled=true is available only if removing unsubscribe link is approved for the user account by support. More on this here. When custom_unsubscribe_url_enabled is skipped on creating a project, it’s value is taken from user
    /// </summary>
    [JsonProperty("custom_unsubscribe_url_enabled", NullValueHandling = NullValueHandling.Ignore)]
    public bool Custom_unsubscribe_url_enabled { get; set; }

    /// <summary>
    /// A unique domain identifier which will determine the tracking domain or dedicated IP pool to be used by default. If the value is not specified, a default system backend domain will be assigned for our account or project instead.
    /// </summary>
    [JsonProperty("backend_id", NullValueHandling = NullValueHandling.Ignore)]
    public int Backend_id { get; set; }

    public ProjectData()
    {
    }

    private ProjectData(string id, string name, string country, DateTime reg_time, bool send_enabled,
        bool custom_unsubscribe_url_enabled, int backendId)
    {
        Id = id;
        Name = name;
        Country = country;
        Reg_time = reg_time;
        Send_enabled = send_enabled;
        Custom_unsubscribe_url_enabled = custom_unsubscribe_url_enabled;
        Backend_id = backendId;
    }

    public static ProjectData CreateNew(string id, string name, string country, DateTime reg_time, bool send_enabled,
        bool custom_unsubscribe_url_enabled, int backendId)
    {
        return new ProjectData(id, name, country, reg_time, send_enabled, custom_unsubscribe_url_enabled, backendId);
    }

    public static ProjectData CreateNew(string name, string country, bool send_enabled,
        bool custom_unsubscribe_url_enabled, int backendId)
    {
        return new ProjectData("", name, country, DateTime.Now, send_enabled, custom_unsubscribe_url_enabled, backendId);
    }

    public static ProjectData CreateNew(string id, string name, string country, bool send_enabled,
        bool custom_unsubscribe_url_enabled, int backendId)
    {
        return new ProjectData(id, name, country, DateTime.Now, send_enabled, custom_unsubscribe_url_enabled, backendId);
    }

    public string ToJson()
    {
        Dictionary<string, object> jsonObject = new Dictionary<string, object>
        {
            ["project"] = new Dictionary<string, object>()
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

public class ProjectInputData
{
    private string? _status { get; set; }
    private string? _project_id { get; set; }
    private string? _project_api_key { get; set; }

    /// <summary>
    /// Unqiue project identifier, ASCII string up to 36 characters long.
    /// </summary>
    public string? Project_Id => _project_id;

    /// <summary>
    /// API key of the project. You can use it instead of the user API key parameter in all methods except project/* methods.
    /// </summary>
    public string? Project_api_key => _project_api_key;

    /// <summary>
    /// “success” string
    /// </summary>
    public string? Status => _status;

    private ProjectInputData()
    {
    }

    private ProjectInputData(string project_id, string project_api_key, string status = "")
    {
        _project_id = project_id;
        _project_api_key = project_api_key;
        _status = status;
    }

    public static ProjectInputData CreateNew(string project_id, string project_api_key, string status = "")
    {
        return new ProjectInputData(project_id, project_api_key, status);
    }
}

public class ProjectDataList
{
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? status { get; set; }

    [JsonProperty("projects", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<ProjectData>? Projects { get; set; }
}