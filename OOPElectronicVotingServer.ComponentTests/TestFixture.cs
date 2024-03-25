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

public sealed class ApiWebApplicationFactory<THandler> : WebApplicationFactory<Program>
    where THandler : AuthenticationHandler<AuthenticationSchemeOptions>
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
                .AddScheme<AuthenticationSchemeOptions, THandler>("Test", _ => { });
        });
    }
}

public class VoterAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(
            new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, "TestAccount"), new Claim(ClaimTypes.NameIdentifier, "TestAccount"),
                    new Claim(ClaimTypes.Email, "TestAccount")
                }, "Test")), "Test")));
    }
}

public class AdminAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options,
        logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(
            new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, "AdminTestAccount"),
                    new Claim(ClaimTypes.NameIdentifier, "AdminTestAccount"),
                    new Claim(ClaimTypes.Email, "AdminTestAccount"), new Claim(ClaimTypes.Role, "Admin")
                }, "Test")), "Test")));
    }
}

public class UnauthorisedHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options,
        logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.Fail("Unauthorised"));
    }
}

public sealed class TestFixture : IDisposable
{
    public HttpClient VoterClient { get; }
    public HttpClient AdminClient { get; }
    public HttpClient UnauthorisedClient { get; }

    private readonly ApiWebApplicationFactory<VoterAuthHandler> _voterFactory;
    private readonly ApiWebApplicationFactory<AdminAuthHandler> _adminFactory;
    private readonly ApiWebApplicationFactory<UnauthorisedHandler> _unauthorisedFactory;

    public TestFixture()
    {
        _voterFactory = new ApiWebApplicationFactory<VoterAuthHandler>();
        VoterClient = _voterFactory.CreateClient();

        _adminFactory = new ApiWebApplicationFactory<AdminAuthHandler>();
        AdminClient = _adminFactory.CreateClient();

        _unauthorisedFactory = new ApiWebApplicationFactory<UnauthorisedHandler>();
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