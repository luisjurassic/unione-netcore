# unione-csharp
UniOne Email API C# library for sending emails with unione.io email service.

## Installation
Add UniOne library to your project references.
.Net 7 is required.

## Usage
###Configuration
First step is adding necessary configuration to your appsettings.json file.

```
{
 "UniOne": 
  {
    "ServerAddress": "https://eu1.unione.io/",
    "ApiUrl": "en/transactional/api/",
    "ApiVersion": "v1",
    "X-API-KEY": "",
    "ServerTimeout": 5,
    "EnableLogging": true
  }
}
```

Where:

**ServerAddress** - __eu1.unione.io__ for EU or __us1.unione.io__ for USA & Canada
**X-API-KEY** - Api key generated in Account -> Security tab on your UniOne account
**ServerTimeout** - Default 5 seconds timeout
**EnableLogging** - Set to true if you want to generate logs for library

### Creating UniOne instance

```
var uniOne = new UniOne.UniOne(configuration);
```
UniOne constructor needs IConfiguration interface with loaded appsettings.json

```
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
```

### UniOne main classes
Library implements those classes with their respective methods. For more detail refer to [UniOne](https://docs.unione.io/en/web-api) api documentations.

**Email**
Method related to sending emails.
- async Task<EmailResponseData> Send(EmailMessageData message)
- async Task<IOperationResult<string>> Subscribe(string fromEmail, string fromName, string toEmail)

**EmailValidation**
- async Task<EmailValidationData> ValidationSingle(string emailAddress)

**Template**
- async Task<IOperationResult<string>> Set(TemplateData templateData)
- async Task<TemplateData> Get(string id)
- async Task<TemplateList> List(int limit = 50, int offset = 0)
- async Task<IOperationResult<string>> Detele(string id)

**Webhook**
- async Task<IOperationResult<string>> Set(WebhookData webhookData)
- async Task<WebhookData> Get(string url)
- async Task<WebhookData> List(int limit = 50, int offset = 0)
- async Task<IOperationResult<string>> Delete(string url)

**Suppression**
- async Task<SuppressionData> Set(string email, string cause, DateTime created)
- async Task<SuppressionData> Get(string email, bool all_projects)
- async Task<SuppressionData> List(string cause ="" , string source = "" , DateTime? start_time = null, string cursor = "", int limit = 50)
- async Task<SuppressionData> Delete(string email)

**Domain**
- async Task<DomainData> GetDNSRecords(string domain)
- async Task<DomainData> ValidateVerificationRecord(string domain)
- async Task<DomainData> ValidateDkim(string domain)
- async Task<DomainList> List(string domain, int limit = 50,int offset = 0 )

**EventDump**
- async Task<IOperationResult<string>> Create(EventDumpRequest request)
- async Task<EventDumpRequest> Get(string dumpId)
- async Task<EventDumpList> List(int limit = 50, int offset = 0)
- async Task<IOperationResult<string>> Detele(string dumpId)

**Tag**
- async Task<TagList> List()
- async Task<IOperationResult<string>> Detele(int tagId)

**Project**
- async Task<ProjectInputData> Create(string name, string country, bool send_enabled, bool custom_unsubscribe_url_enabled, int backendId)
- async Task<ProjectInputData> Update(string id,string name, string country, bool send_enabled, bool custom_unsubscribe_url_enabled, int backendId)
- async Task<ProjectDataList> List(string project_id = "" , string project_api_key = "")
- async Task<IOperationResult<string>> Delete(string id, string project_api_key)

**System**
- async Task<SystemInfoData> SystemInfo()

**Obsolete**
- async Task<UnsubscribedData> UnsubscribedSet(string emailAddress)
- async Task<UnsubscribedData> UnsubscribedCheck(string emailAddress)
- async Task<UnsubscribedList> UnsubscribedList(string emailAddress)

**Generic**
- async Task<T> CustomRequest<T>(string request, object obj, Func<string, string, OperationResult<T>> operationResultCreator) where T : class - method allowing to send custom request to UniOne API 

Before you send custom request, you have to provide expected class for method result.
```
var customRequest = await uniOne.Generic.CustomRequest<TemplateList>("template/list.json", "{\"limit\": 50,\"offset\":0}", (status, response) => OperationResult<TemplateList>.CreateNew(status, response));

if (customRequest == null)
{
    var error = uniOne.Generic.GetError();
}

```

