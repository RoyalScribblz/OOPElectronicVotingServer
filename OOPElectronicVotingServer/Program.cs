using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Endpoints.BallotEndpoints;
using OOPElectronicVotingServer.Endpoints.CandidateEndpoints;
using OOPElectronicVotingServer.Endpoints.ElectionEndpoints;
using OOPElectronicVotingServer.Endpoints.VoterEndpoints;

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

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("LocalDev");
}              

app.UseHttpsRedirection();

app.MapCreateBallot();
app.MapGetBallots();
app.MapCreateVoter();
app.MapGetVoter();
app.MapCreateCandidate();
app.MapGetCandidates();
app.MapCreateElection();
app.MapGetElections();

app.Run();