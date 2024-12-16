using System.Threading.Tasks;
using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;

namespace Unione.Net.Services;

public class Email(IApiConnection apiConnection, IMapper mapper, ILogger logger)
{
    private ErrorData? _error;

    public async Task<EmailResponseData> Send(EmailMessageData message)
    {
        _error = null;
        if (apiConnection.IsLoggingEnabled())
            logger.Information("Email:Send");

        (string, string) apiResponse = await apiConnection.SendMessageAsync("email/send.json", message.ToJson());
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<EmailResponseData> result = OperationResult<EmailResponseData>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (apiConnection.IsLoggingEnabled())
                logger.Information("Email:Send:result:" + result.GetStatus());

            dynamic? mappedResult = mapper.Map<EmailResponseData>(result.GetResponse());

            if (apiConnection.IsLoggingEnabled())
                logger.Information("Email:Send:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (apiConnection.IsLoggingEnabled())
                logger.Information("Email:Send:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (apiConnection.IsLoggingEnabled())
                logger.Information("Email:Send:END");

            return null!;
        }
    }

    public async Task<IOperationResult<string>> Subscribe(string fromEmail, string fromName, string toEmail)
    {
        _error = null;
        if (apiConnection.IsLoggingEnabled())
            logger.Information("Email:Subscribe:fromEmail[" + fromEmail + "]:fromName[" + fromName + "]:toEmail[" + toEmail + "]");

        (string, string) apiResponse = await apiConnection.SendMessageAsync("email/send.json", EmailSubscribeData.CreateNew(fromEmail, fromName, toEmail));
        if (!apiResponse.Item1.ToLower().Contains("error") && !apiResponse.Item2.ToLower().Contains("error") && !apiResponse.Item1.ToLower().Contains("cancelled"))
        {
            OperationResult<string> result = OperationResult<string>.CreateNew(apiResponse.Item1, apiResponse.Item2);
            if (apiConnection.IsLoggingEnabled())
                logger.Information("Email:Subscribe:result:" + result.GetStatus());

            dynamic? mappedResult = mapper.Map<EmailResponseData>(result.GetResponse());

            if (apiConnection.IsLoggingEnabled())
                logger.Information("Email:Subscribe:END");

            return mappedResult;
        }
        else
        {
            OperationResult<ErrorDetailsData> result = OperationResult<ErrorDetailsData>.CreateNew(apiResponse.Item1, apiResponse.Item2);

            if (apiConnection.IsLoggingEnabled())
                logger.Information("Email:Subscribe:result:" + result.GetStatus());

            _error = new ErrorData
            {
                Status = apiResponse.Item1
            };
            if (!_error.Status.Contains("timeout"))
                _error.Details = mapper.Map<ErrorDetailsData>(result.GetResponse());
            else
                _error.Details = ErrorDetailsData.CreateNew("TIMEOUT", apiResponse.Item1, 0);

            if (apiConnection.IsLoggingEnabled())
                logger.Information("Email:Subscribe:END");

            return null!;
        }
    }

    public ErrorData? GetError()
    {
        return _error;
    }
}