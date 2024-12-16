using System;
using System.Threading.Tasks;
using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;

namespace Unione.Net.Services;

public class Generic
{
    private readonly IApiConnection _apiConnection;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private ErrorData? _error;

    public Generic(IApiConnection apiConnection, IMapper mapper, ILogger logger)
    {
        _apiConnection = apiConnection;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<T> CustomRequest<T>(string request, object obj, Func<string, string, OperationResult<T>> operationResultCreator) where T : class
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Generic:CustomRequest");

        (string, string) apiResponse = await _apiConnection.SendMessageAsync(request, obj);
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<T>? result = operationResultCreator(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Generic:CustomRequest:" + result.GetStatus());

            dynamic? mappedResult = _mapper.Map<T>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Generic:CustomRequest:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Generic:CustomRequest:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Generic:CustomRequest:END");

            return default!;
        }
    }

    public ErrorData? GetError()
    {
        return _error;
    }
}