using System.Threading.Tasks;
using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;

namespace Unione.Net.Services;

public class Domain
{
    private readonly IApiConnection _apiConnection;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private ErrorData? _error;

    public Domain(IApiConnection apiConnection, IMapper mapper, ILogger logger)
    {
        _apiConnection = apiConnection;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<DomainData> GetDNSRecords(string domain)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Domain:GetDNSRecords:domain[" + domain + "]");

        var apiResponse = await _apiConnection.SendMessageAsync("domain/get-dns-records.json", DomainData.CreateNew(domain));

        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            var result = OperationResult<DomainData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:GetDNSRecords:result:" + result.GetStatus());

            var mappedResult = _mapper.Map<DomainData>(result);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:GetDNSRecords:END");
            return mappedResult;
        }
        else
        {
            var result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:GetDNSRecords:result:" + result.GetStatus());

            _error = new ErrorData();
            _error.Status = apiResponse.Item1;
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:GetDNSRecords:END");

            return null!;
        }
    }

    public async Task<DomainData> ValidateVerificationRecord(string domain)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Domain:ValidateVerificationRecord:domain[" + domain + "]");

        var apiResponse = await _apiConnection.SendMessageAsync("domain/validate-verification-record.json", DomainData.CreateNew(domain));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            var result = OperationResult<DomainData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:ValidateVerificationRecord:result:" + result.GetStatus());

            var mappedResult = _mapper.Map<DomainData>(result);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:ValidateVerificationRecord:END");
            return mappedResult;
        }
        else
        {
            var result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:ValidateVerificationRecord:result:" + result.GetStatus());

            _error = new ErrorData();
            _error.Status = apiResponse.Item1;
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:ValidateVerificationRecord:END");

            return null!;
        }
    }

    public async Task<DomainData> ValidateDkim(string domain)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Domain:ValidateDkim:domain[" + domain + "]");

        var apiResponse = await _apiConnection.SendMessageAsync("domain/validate-dkim.json", DomainData.CreateNew(domain));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            var result = OperationResult<DomainData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:ValidateDkim:result:" + result.GetStatus());

            var mappedResult = _mapper.Map<DomainData>(result);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:ValidateDkim:END");
            return mappedResult;
        }
        else
        {
            var result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:ValidateDkim:result:" + result.GetStatus());

            _error = new ErrorData();
            _error.Status = apiResponse.Item1;
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:ValidateDkim:END");

            return null!;
        }
    }

    public async Task<DomainList> List(string domain, int limit = 50, int offset = 0)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("Domain:List:domain[" + domain + "]:limit[" + limit + "]:offset[" + offset + "]");

        var apiResponse = await _apiConnection.SendMessageAsync("domain/list.json", DomainData.CreateNew(domain, limit, offset));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            var result = OperationResult<DomainList>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:List:result:" + result.GetStatus());

            var mappedResult = _mapper.Map<DomainList>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:List:END");
            return mappedResult;
        }
        else
        {
            var result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:List:result:" + result.GetStatus());

            _error = new ErrorData();
            _error.Status = apiResponse.Item1;
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("Domain:List:END");

            return null!;
        }
    }

    public ErrorData? GetError()
    {
        return _error;
    }
}