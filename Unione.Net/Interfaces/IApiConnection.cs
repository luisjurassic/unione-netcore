using System.Threading.Tasks;

namespace Unione.Net.Interfaces;

public interface IApiConnection
{
    Task<(string, string)> SendMessageAsync(string command, object requestBody);
    bool IsLoggingEnabled();
}