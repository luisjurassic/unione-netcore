using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Unione.Net.Exceptions;

namespace Unione.Net.Models;

public class EmailRecipientData
{
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// Recipient email
    /// </summary>
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Object to pass the substitutions(merge tags) for the recipient (e.g. recipient name, discount code, password change link, etc. See Template engines). The substitutions can be used in the following parameters:
    ///    body.html
    ///    body.plaintext
    ///    body.amp
    ///    subject
    ///    from_name
    ///    headers[“List-Unsubscribe”]
    ///    options.unsubscribe_url
    ///
    /// A substitution name can consist from latin characters, numbers and “_” symbol, and should start with the letter. There’s a special substitution “to_name” which is used to put recipent’s name like “Name Surname” to include it to SMTP header “To” in the form “Name Surname <email@example.com>”. The “to_name” length is limited to 78 symbols.
    /// </summary>
    [JsonProperty("substitutions", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<object, object>? Substitutions { get; set; }

    /// <summary>
    /// Object for passing the metadata, such as “key”: “value”. Max key quantity: 10. Max key length: 64 symbols. Max value length: 1024 symbols. The metadata is returned by webhook and event-dump methods. If you pass strign up to 128 bit with “campaign_id” field name (name is configurable thru support), it will be considered as campaign identifier in statistics.
    /// </summary>
    [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? Metadata { get; set; }

    public EmailRecipientData()
    {
    }

    private EmailRecipientData(string name, string emailAddress, Dictionary<object, object>? substitutions)
    {
        Name = name;
        EmailAddress = emailAddress;
        Substitutions = substitutions;
    }

    public static EmailRecipientData CreateRecipient(string name, string emailAddress, Dictionary<object, object>? substitutions)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        Match match = regex.Match(emailAddress);

        if (match.Success)
            return new EmailRecipientData(name, emailAddress, substitutions);
        else
            throw new IncorrectEmailAdressException($"{emailAddress} is not valid email address!");
    }
}