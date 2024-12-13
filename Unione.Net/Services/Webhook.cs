using System.Threading.Tasks;
using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;

namespace Unione.Net.Services;

public class Webhook
{
    private readonly IApiConnection _apiConnection;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private ErrorData? _error;

    public Webhook(IApiConnection apiConnection, IMapper mapper, ILogger logger)
    {
        _apiConnection = apiConnection;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IOperationResult<WebhookData>> Set(WebhookData webhookData)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Webhook:Set");

        var apiResponse = await _apiConnection.SendMessageAsync("webhook/set.json", webhookData);
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            var result = OperationResult<WebhookData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Set:result:" + result.GetStatus());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Set:END");

            return result;
        }
        else
        {
            var result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Set:result:" + result.GetStatus());

            _error = new ErrorData();
            _error.Status = apiResponse.Item1;
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Set:END");

            return null!;
        }
    }

    public async Task<WebhookData> Get(string url)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Webhook:Get:url[" + url + "]");

        var apiResponse = await _apiConnection.SendMessageAsync("webhook/get.json", "{ \"url\" : \"" + url + "\"  }");
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            var result = OperationResult<WebhookData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Get:result:" + result.GetStatus());

            var mappedResult = _mapper.Map<WebhookData>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Get:END");

            return mappedResult;
        }
        else
        {
            var result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Get:result:" + result.GetStatus());

            _error = new ErrorData();
            _error.Status = apiResponse.Item1;
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Get:END");

            return null!;
        }
    }

    public async Task<WebhookData> List(int limit = 50, int offset = 0)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Webhook:List:limit[" + limit + "]:offset[" + offset + "]");

        var apiResponse = await _apiConnection.SendMessageAsync("webhook/list.json", InputData.CreateNew(null, limit, offset));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            var result = OperationResult<WebhookData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:List:result:" + result.GetStatus());

            var mappedResult = _mapper.Map<WebhookData>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:List:END");

            return mappedResult;
        }
        else
        {
            var result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:List:result:" + result.GetStatus());

            _error = new ErrorData();
            _error.Status = apiResponse.Item1;
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:List:END");

            return null!;
        }
    }

    public async Task<IOperationResult<string>> Delete(string url)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Webhook:Delete:Detele[" + url + "]");

        var apiResponse = await _apiConnection.SendMessageAsync("webhook/delete.json", "{ \"url:\" \"" + url + "\"  }");
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            var result = OperationResult<string>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Delete:result:" + result.GetStatus());

            var mappedResult = _mapper.Map<string>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Delete:END");

            return mappedResult;
        }
        else
        {
            var result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Delete:result:" + result.GetStatus());

            _error = new ErrorData();
            _error.Status = apiResponse.Item1;
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Webhook:Delete:END");

            return null!;
        }
    }

    public ErrorData? GetError()
    {
        return _error;
    }
}