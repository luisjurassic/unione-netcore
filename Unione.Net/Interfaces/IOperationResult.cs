namespace Unione.Net.Interfaces;

public interface IOperationResult<T> where T : class
{
    string? GetStatus();
    string? GetMessage();
    dynamic? GetResponse();
}