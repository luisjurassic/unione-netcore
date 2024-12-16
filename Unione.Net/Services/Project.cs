using System.Threading.Tasks;
using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;

namespace Unione.Net.Services;

public class Project
{
    private readonly IApiConnection _apiConnection;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private ErrorData? _error;

    public Project(IApiConnection apiConnection, IMapper mapper, ILogger logger)
    {
        _apiConnection = apiConnection;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProjectInputData> Create(string name, string country, bool send_enabled, bool custom_unsubscribe_url_enabled, int backendId)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Project:Create:name[" + name + "]:country[" + country + "]:sendEnabled[" + send_enabled + "]:customUnsubscribeUrlEnabled[" + custom_unsubscribe_url_enabled + "]:backendId[" + backendId + "]");

        (string, string) apiResponse = await _apiConnection.SendMessageAsync("project/create.json", ProjectData.CreateNew(name, country, send_enabled, custom_unsubscribe_url_enabled, backendId).ToJson());
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<ProjectInputData> result = OperationResult<ProjectInputData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Create:result:" + result.GetStatus());

            dynamic? mappedResult = _mapper.Map<ProjectInputData>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Create:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Create:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Create:END");

            return null!;
        }
    }

    public async Task<ProjectInputData> Update(string id, string name, string country, bool send_enabled, bool custom_unsubscribe_url_enabled, int backendId)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Project:Update:id[" + id + ":name[" + name + "]:country[" + country + "]:sendEnabled[" + send_enabled + "]:customUnsubscribeUrlEnabled[" + custom_unsubscribe_url_enabled + "]:backendId[" + backendId + "]");

        (string, string) apiResponse = await _apiConnection.SendMessageAsync("project/update.json", ProjectData.CreateNew(id, name, country, send_enabled, custom_unsubscribe_url_enabled, backendId));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<ProjectInputData> result = OperationResult<ProjectInputData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Update:result:" + result.GetStatus());

            dynamic? mappedResult = _mapper.Map<ProjectInputData>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Update:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Update:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Update:END");

            return null!;
        }
    }

    public async Task<ProjectDataList> List(string project_id = "", string project_api_key = "")
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Project:List:project_id[" + project_id + "]");

        object? body = null;
        if (string.IsNullOrEmpty(project_id) || string.IsNullOrEmpty(project_api_key))
            body = "{}";
        else
            body = ProjectInputData.CreateNew(project_id, project_api_key);

        (string, string) apiResponse = await _apiConnection.SendMessageAsync("domain/list.json", body);
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<ProjectDataList> result = OperationResult<ProjectDataList>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:List:result:" + result.GetStatus());

            dynamic? mappedResult = _mapper.Map<ProjectDataList>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:List:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:List:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:List:END");

            return null!;
        }
    }

    public async Task<IOperationResult<string>> Delete(string id, string project_api_key)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Project:Delete:id[" + id + "]");

        (string, string) apiResponse = await _apiConnection.SendMessageAsync("project/delete.json", ProjectInputData.CreateNew(id, project_api_key));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<string> result = OperationResult<string>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Delete:result:" + result.GetStatus());

            dynamic? mappedResult = _mapper.Map<string>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Delete:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Delete:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Project:Delete:END");

            return null!;
        }
    }

    public ErrorData? GetError()
    {
        return _error;
    }
}