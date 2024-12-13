using System;
using Newtonsoft.Json;

namespace Unione.Net.Models;

public class AccountingData
{
    /// <summary>
    /// Date and time of accounting period start in UTC in “YYYY-MM-DD hh:mm:ss” format.
    /// </summary>
    [JsonProperty("period_start", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime PeriodStart { get; set; }

    /// <summary>
    /// Date and time of accounting period end in UTC in “YYYY-MM-DD hh:mm:ss” format.
    /// </summary>
    [JsonProperty("period_end", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime PeriodEnd { get; set; }

    /// <summary>
    /// Number of emails included into accounting period.
    /// </summary>
    [JsonProperty("emails_included", NullValueHandling = NullValueHandling.Ignore)]
    public int EmailsIncluded { get; set; }

    /// <summary>
    /// Number of emails sent during accounting period. Can be bigger than emails_included in case of overage.
    /// </summary>
    [JsonProperty("emails_sent", NullValueHandling = NullValueHandling.Ignore)]
    public int EmailsSent { get; set; }

    public AccountingData()
    {
    }

    private AccountingData(DateTime periodStart, DateTime periodEnd, int emailsIncluded, int emailsSent)
    {
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        EmailsIncluded = emailsIncluded;
        EmailsSent = emailsSent;
    }

    public static AccountingData CreateNew(DateTime periodStart, DateTime periodEnd, int emailsIncluded, int emailsSent)
    {
        return new AccountingData(periodStart, periodEnd, emailsIncluded, emailsSent);
    }
}