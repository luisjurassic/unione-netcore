namespace Unione.Net.Models;

public class EmailAddressData
{
    public string? Address { get; set; }

    public EmailAddressData()
    {
    }

    private EmailAddressData(string address)
    {
        Address = address;
    }

    public static EmailAddressData CreateNew(string emailAddress)
    {
        return new EmailAddressData(emailAddress);
    }
}