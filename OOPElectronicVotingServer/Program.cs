using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Endpoints.BallotEndpoints;
using OOPElectronicVotingServer.Endpoints.CandidateEndpoints;
using OOPElectronicVotingServer.Endpoints.ElectionEndpoints;
using OOPElectronicVotingServer.Endpoints.VoterEndpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VotingDatabase>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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