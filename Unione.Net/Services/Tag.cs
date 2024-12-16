using System.Threading.Tasks;
using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;

namespace Unione.Net.Services;

public class Tag
{
    private readonly IApiConnection _apiConnection;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private ErrorData? _error;

    public Tag(IApiConnection apiConnection, IMapper mapper, ILogger logger)
    {
        _apiConnection = apiConnection;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TagList> List()
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Tag:List");

        (string, string) apiResponse = await _apiConnection.SendMessageAsync("tag/list.json", "{ }");
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<TagList> result = OperationResult<TagList>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Tag:List:result:" + result.GetStatus());

            dynamic? mappedResult = _mapper.Map<TagList>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Tag:List:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Tag:List:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Tag:List:END");

            return null!;
        }
    }

    public async Task<IOperationResult<string>> Detele(int tagId)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Tag:Delete:tagId[" + tagId + "]");

        (string, string) apiResponse = await _apiConnection.SendMessageAsync("tag/delete.json", TagData.CreateNew(tagId, ""));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<string> result = OperationResult<string>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Tag:Delete:result:" + result.GetStatus());

            dynamic? mappedResult = _mapper.Map<string>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Tag:Delete:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Tag:Delete:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Tag:Delete:END");

            return null!;
        }
    }

    public ErrorData? GetError()
    {
        return _error;
    }
}