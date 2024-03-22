using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Endpoints;
using OOPElectronicVotingServer.Extensions;
using OOPElectronicVotingServer.Services.BallotService;
using OOPElectronicVotingServer.Services.CandidateService;
using OOPElectronicVotingServer.Services.ElectionService;
using OOPElectronicVotingServer.Services.UserService;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
    
    if (bool.Parse(builder.Configuration["Auth0:DevelopmentEnvironment"]!))
    {
        options.RequireHttpsMetadata = false;
    }
});

builder.Services.AddAuthorization()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddDbContext<VotingDatabase>();

builder.Services.AddCors(options => options.AddPolicy(name: "LocalDev", policy =>
{
    policy.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback)
          .AllowAnyHeader()
          .AllowAnyMethod();
}));

builder.Services.AddScoped<IBallotService, BallotService>()
                .AddScoped<ICandidateService, CandidateService>()
                .AddScoped<IElectionService, ElectionService>()
                .AddScoped<IUserService, UserService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI()
       .UseCors("LocalDev");
}              

app.UseAuthentication()
   .UseAuthorization()
   .UseHttpsRedirection();

app.MapBallotEndpoints()
   .MapCandidateEndpoints()
   .MapElectionEndpoints()
   .MapUserEndpoints();

await app.Services.CreateScope().ServiceProvider
                  .GetRequiredService<VotingDatabase>()
                  .Seed();

app.Run();