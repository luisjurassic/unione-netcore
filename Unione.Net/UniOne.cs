using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;
using Unione.Net.Services;
using EventDump = Unione.Net.Services.EventDump;

namespace Unione.Net;

public class UniOne : IUniOne
{
    private readonly Domain _domain;
    private readonly Email _email;
    private readonly EmailValidation _emailValidation;
    private readonly EventDump _eventDump;
    private readonly Obsolete _obsolete;
    private readonly Project _project;
    private readonly Suppression _suppression;
    private readonly Services.System _system;
    private readonly Tag _tag;
    private readonly Template _template;
    private readonly Webhook _webhook;
    private readonly Generic _generic;

    public Domain Domain => _domain;
    public Email Email => _email;
    public EmailValidation EmailValidation => _emailValidation;
    public EventDump EventDump => _eventDump;
    public Obsolete Obsolete => _obsolete;
    public Project Project => _project;
    public Suppression Suppression => _suppression;
    public Services.System System => _system;
    public Tag Tag => _tag;
    public Template Template => _template;
    public Webhook Webhook => _webhook;
    public Generic Generic => _generic;

    public UniOne(Configuration configuration)
    {
        ILogger logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("unione.net.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => { });

        IMapper? mapper = mapperConfiguration.CreateMapper();

        IApiConfiguration apiConfiguration = ApiConfiguration.CreateNew(configuration.ServerAddress, configuration.ApiUrl, configuration.ApiVersion,
            configuration.ApiKey, configuration.EnableLogging, configuration.ServerTimeout);

        IApiConnection apiConnection = new ApiConnection(apiConfiguration);


        _domain = new Domain(apiConnection, mapper, logger);
        _email = new Email(apiConnection, mapper, logger);
        _emailValidation = new EmailValidation(apiConnection, mapper, logger);
        _eventDump = new EventDump(apiConnection, mapper, logger);
        _obsolete = new Obsolete(apiConnection, mapper, logger);
        _project = new Project(apiConnection, mapper, logger);
        _suppression = new Suppression(apiConnection, mapper, logger);
        _system = new Services.System(apiConnection, mapper, logger);
        _tag = new Tag(apiConnection, mapper, logger);
        _template = new Template(apiConnection, mapper, logger);
        _webhook = new Webhook(apiConnection, mapper, logger);
        _generic = new Generic(apiConnection, mapper, logger);
    }
}