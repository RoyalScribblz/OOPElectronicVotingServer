using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Extensions;
using OOPElectronicVotingServer.Endpoints;
using OOPElectronicVotingServer.Services;
using OOPElectronicVotingServer.Services.Abstractions;

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

builder.Services.AddSingleton<IElectionService, ElectionService>();

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
    .MapVoterEndpoints();

await app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<VotingDatabase>()
    .Seed();

app.Run();