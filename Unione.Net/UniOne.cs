using AutoMapper;
using Serilog;
using Unione.Net.Interfaces;
using Unione.Net.Models;
using Unione.Net.Services;
using EventDump = Unione.Net.Services.EventDump;

namespace Unione.Net;

public class UniOne : IUniOne
{
    private ILogger _logger;
    private IMapper _mapper;
    private IApiConfiguration _apiConfiguration;
    private IApiConnection _apiConnection;

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
        _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("UniOne-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var mapperConfiguration = new MapperConfiguration(cfg => { });

        _mapper = mapperConfiguration.CreateMapper();

        _apiConfiguration =
            ApiConfiguration.CreateNew(configuration.ServerAddress, configuration.ApiUrl, configuration.ApiVersion,
                configuration.ApiKey, configuration.EnableLogging, configuration.ServerTimeout);

        _apiConnection = new ApiConnection(_apiConfiguration);


        _domain = new Domain(_apiConnection, _mapper, _logger);
        _email = new Email(_apiConnection, _mapper, _logger);
        _emailValidation = new EmailValidation(_apiConnection, _mapper, _logger);
        _eventDump = new EventDump(_apiConnection, _mapper, _logger);
        _obsolete = new Obsolete(_apiConnection, _mapper, _logger);
        _project = new Project(_apiConnection, _mapper, _logger);
        _suppression = new Suppression(_apiConnection, _mapper, _logger);
        _system = new Services.System(_apiConnection, _mapper, _logger);
        _tag = new Tag(_apiConnection, _mapper, _logger);
        _template = new Template(_apiConnection, _mapper, _logger);
        _webhook = new Webhook(_apiConnection, _mapper, _logger);
        _generic = new Generic(_apiConnection, _mapper, _logger);
    }
}