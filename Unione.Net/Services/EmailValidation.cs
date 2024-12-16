using System.Threading.Tasks;
using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;

namespace Unione.Net.Services;

public class EmailValidation
{
    private readonly IApiConnection _apiConnection;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private ErrorData? _error;

    public EmailValidation(IApiConnection apiConnection, IMapper mapper, ILogger logger)
    {
        _apiConnection = apiConnection;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<EmailValidationData> ValidationSingle(string emailAddress)
    {
        _error = null;
        if (_apiConnection.IsLoggingEnabled())
            _logger.Information("EmailValidation:ValidationSingle:emailAddress[" + emailAddress + "]");

        (string, string) apiResponse = await _apiConnection.SendMessageAsync("email-validation/single.json", EmailAddressData.CreateNew(emailAddress));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<EmailValidationData> result = OperationResult<EmailValidationData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("EmailValidation:ValidationSingle:result:" + result.GetStatus());

            dynamic? mappedResult = _mapper.Map<EmailValidationData>(result.GetResponse());

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("EmailValidation:ValidationSingle:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("EmailValidation:ValidationSingle:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = _mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (_apiConnection.IsLoggingEnabled())
                _logger.Information("EmailValidation:ValidationSingle:END");

            return null!;
        }
    }

    public ErrorData? GetError()
    {
        return _error;
    }
}