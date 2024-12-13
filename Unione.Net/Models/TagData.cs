using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class TagData
{
    /// <summary>
    /// Unique tag id.
    /// </summary>
    [JsonProperty("tag_id", NullValueHandling = NullValueHandling.Ignore)]
    public int TagId { get; set; }

    /// <summary>
    /// Tag name.
    /// </summary>
    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    public string? Tag { get; set; }

    public TagData()
    {
    }

    private TagData(int tagId, string tag)
    {
        TagId = tagId;
        Tag = tag;
    }

    public static TagData CreateNew(int tagId, string tag)
    {
        return new TagData(tagId, tag);
    }
}

public class TagList
{
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<TagData>? Tags { get; set; }
}