using Newtonsoft.Json;
using Unione.Net.Interfaces;

namespace Unione.Net;

public class OperationResult<T> : IOperationResult<T> where T : class
{
    private dynamic? _reponseBody { get; set; }
    private string? _status { get; set; }

    public string? GetStatus()
    {
        return _status;
    }

    public string? GetMessage()
    {
        return _reponseBody?.message ? _reponseBody?.message : string.Empty;
    }

    public dynamic? GetResponse()
    {
        return _reponseBody;
    }

    private OperationResult()
    {
    }

    private OperationResult(string status, dynamic? responseBody)
    {
        _status = status;
        _reponseBody = responseBody;
    }

    public static OperationResult<T> CreateNew(string status, string responseBody)
    {
        var data = JsonConvert.DeserializeObject<T>(responseBody);

        return new OperationResult<T>(status, data);
    }
}