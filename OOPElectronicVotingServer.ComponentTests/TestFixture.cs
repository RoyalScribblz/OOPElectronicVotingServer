using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OOPElectronicVotingServer.ComponentTests;

public sealed class VoterApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            })
            .AddScheme<AuthenticationSchemeOptions, VoterTestAuthHandler>("Test", _ => { });
        });
    }
}

public class VoterTestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestAccount"), new Claim(ClaimTypes.NameIdentifier, "TestAccount"), new Claim(ClaimTypes.Email, "TestAccount") }, "Test")), "Test")));
    }

    public VoterTestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public VoterTestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }
}

public sealed class AdminApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            })
            .AddScheme<AuthenticationSchemeOptions, AdminTestAuthHandler>("Test", _ => { });
        });
    }
}

public class AdminTestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "AdminTestAccount"), new Claim(ClaimTypes.NameIdentifier, "AdminTestAccount"), new Claim(ClaimTypes.Email, "AdminTestAccount"), new Claim(ClaimTypes.Role, "Admin") }, "Test")), "Test")));
    }

    public AdminTestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public AdminTestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }
}

public sealed class UnauthorisedApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            })
            .AddScheme<AuthenticationSchemeOptions, UnauthorisedTestAuthHandler>("Test", _ => { });
        });
    }
}

public class UnauthorisedTestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.Fail("Unauthorised"));
    }

    public UnauthorisedTestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public UnauthorisedTestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }
}

public sealed class TestFixture : IDisposable
{
    // TODO make this nice and OOP
    public HttpClient VoterClient { get; }
    public HttpClient AdminClient { get; }
    public HttpClient UnauthorisedClient { get; }
    
    private readonly VoterApiWebApplicationFactory _voterFactory;
    private readonly AdminApiWebApplicationFactory _adminFactory;
    private readonly UnauthorisedApiWebApplicationFactory _unauthorisedFactory;

    public TestFixture()
    {
        _voterFactory = new VoterApiWebApplicationFactory();
        VoterClient = _voterFactory.CreateClient();
        
        _adminFactory = new AdminApiWebApplicationFactory();
        AdminClient = _adminFactory.CreateClient();
        
        _unauthorisedFactory = new UnauthorisedApiWebApplicationFactory();
        UnauthorisedClient = _unauthorisedFactory.CreateClient();
    }

    public void Dispose()
    {
        VoterClient.Dispose();
        AdminClient.Dispose();
        UnauthorisedClient.Dispose();
        
        _voterFactory.Dispose();
        _adminFactory.Dispose();
        _unauthorisedFactory.Dispose();
    }
}