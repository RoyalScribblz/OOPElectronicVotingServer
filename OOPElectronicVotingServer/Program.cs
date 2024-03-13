using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Extensions;
using OOPElectronicVotingServer.Endpoints;
using OOPElectronicVotingServer.Services;
using OOPElectronicVotingServer.Services.BallotService;
using OOPElectronicVotingServer.Services.CandidateService;
using OOPElectronicVotingServer.Services.ElectionService;
using OOPElectronicVotingServer.Services.UserService;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VotingDatabase>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "LocalDev", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IBallotService, BallotService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IElectionService, ElectionService>();
builder.Services.AddScoped<IUserService, UserService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("LocalDev");
}              

app.UseHttpsRedirection();

app.MapBallotEndpoints()
    .MapCandidateEndpoints()
    .MapElectionEndpoints()
    .MapUserEndpoints();

await app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<VotingDatabase>()
    .Seed();

app.Run();