# Unione Net
Baseado em .Net Standard 2.0 essa biblioteca é um fork com base no projeto [unione-csharp](https://github.com/unione-repo/unione-csharp).

## Instalação
Adicione a biblioteca às referências do seu projeto. Apos isso crie uma instância do tipo **Configuration**, passando os dados básicos para comunicação com Unione.

```
Configuration configuration = new Configuration()
        {            
            ServerAddress = "us1.unione.io"
            ApiKey = "...",
            ServerTimeout = 5,
            EnableLogging = true
        };

```
- **ServerAddress**: Você deve enviar uma solicitação de API para o servidor em que sua conta está registrada. __eu1.unione.io__(servidor europeu UniOne) ou __us1.unione.io__(servidor UniOne dos EUA e Canadá).
- **ApiKey**: Chave de API gerada na sua conta UniOne
- **ServerTimeout**: Tempo limite padrão de 30 segundos
- **EnableLogging**: Defina como **true** se quiser gerar logs

Crie uma instância UniOne para usar os métodos implementados

```
        UniOne uni = new UniOne(configuration);
        
        //Envia um email
        EmailResponseData result = await uni.Email.Send(new EmailMessageData
        {
            //preencha as propriedade conforme a sua necessidade...
            FromEmail = "teste@teste.com",
            FromName = "Luis",
            Body = new Body
            {
                Html = "<p>Email de teste</p>"
            }
        });
        
        //Listar os webhooks cadastrados
        WebhookData webhook = await uni.Webhook.List();

```

### Implementações
A biblioteca implementa essas classes com seus respectivos métodos. Para mais detalhes, consulte a documentação da API [UniOne](https://docs.unione.io/en/web-api).

**Email**
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
- async Task<T> CustomRequest<T>(string request, object obj, Func<string, string, OperationResult<T>> operationResultCreator) where T : class

### Licença
A biblioteca está disponível como código aberto sob os termos da [Licença MIT](LICENSE).