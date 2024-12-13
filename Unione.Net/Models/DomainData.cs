using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class DomainData
{
    /// <summary>
    /// Domain to get DNS records for.
    /// </summary>
    [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
    public string? Domain { get; set; }

    public int Limit { get; set; }
    public int Offset { get; set; }

    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    /// <summary>
    /// Record to be added “as is” to verify ownership of this domain.
    /// </summary>
    public string? Verification_record { get; set; }

    /// <summary>
    /// DKIM signature for the domain. This property contains only the key part, you should prepend it with “k=rsa, p=” part for the record to be valid (see example).
    /// </summary>
    public string? sDkim { get; set; }

    /// <summary>
    /// Object describing verification record value and status.
    /// </summary>
    [JsonProperty("verification-record", NullValueHandling = NullValueHandling.Ignore)]
    public VerificationRecord? VerificationRecord { get; set; }

    /// <summary>
    /// Object describing DKIM record value and status.
    /// </summary>
    [JsonProperty("dkim", NullValueHandling = NullValueHandling.Ignore)]
    public DKIM? DKIM { get; set; }

    public DomainData()
    {
    }

    private DomainData(string domain, int limit = 50, int offset = 0)
    {
        Domain = domain;
        Limit = limit;
        Offset = offset;
    }

    public static DomainData CreateNew(string domain, int limit = 50, int offset = 0)
    {
        return new DomainData(domain, limit, offset);
    }
}

public class DomainList
{
    public string? status { get; set; }
    public IEnumerable<DomainData>? domains { get; set; }
}

public class VerificationRecord
{
    /// <summary>
    /// Record to be added “as is” to verify ownership of this domain.
    /// </summary>
    [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
    public string? Value { get; set; }

    /// <summary>
    /// Only domains with “confirmed” verification record are allowed as sender domains.
    /// </summary>
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    public VerificationRecord()
    {
    }

    private VerificationRecord(string value, string status)
    {
        Value = value;
        Status = status;
    }

    public static VerificationRecord CreateNew(string value, string status)
    {
        return new VerificationRecord(value, status);
    }
}

public class DKIM
{
    /// <summary>
    /// DKIM signature for the domain. This property contains only the key part, you should prepend it with “k=rsa, p=” part for the record to be valid (see domain/get-dns-records).
    /// </summary>
    [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
    public string? Key { get; set; }

    /// <summary>
    /// Only domains with “active” DKIM record are allowed as sender domains.
    /// </summary>
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }

    public DKIM()
    {
    }

    private DKIM(string key, string status)
    {
        Key = key;
        Status = status;
    }

    public static DKIM CreateNew(string key, string status)
    {
        return new DKIM(key, status);
    }
}