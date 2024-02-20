using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;
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

var scope = app.Services.CreateScope();

VotingDatabase db = scope.ServiceProvider.GetRequiredService<VotingDatabase>();

Guid idA = Guid.NewGuid(); 
Guid idB = Guid.NewGuid(); 
Guid idC = Guid.NewGuid(); 
        
db.Candidates.AddRange([
    new Candidate
    {
        CandidateId = idA,
        Name = "Candidate A",
        ImageUrl = string.Empty
    },
    new Candidate
    {
        CandidateId = idB,
        Name = "Candidate B",
        ImageUrl = string.Empty
    },
    new Candidate
    {
        CandidateId = idC,
        Name = "Candidate C",
        ImageUrl = string.Empty
    },
]);
        
db.Elections.Add(new Election
{
    ElectionId = Guid.NewGuid(),
    StartTime = DateTime.Today.AddDays(-1),
    EndTime = DateTime.Today.AddDays(1),
    CandidateIds = [idA, idB, idC]
        
});

Guid idD = Guid.NewGuid(); 
Guid idE = Guid.NewGuid(); 
Guid idF = Guid.NewGuid(); 
        
db.Candidates.AddRange([
    new Candidate
    {
        CandidateId = idD,
        Name = "Candidate D",
        ImageUrl = string.Empty
    },
    new Candidate
    {
        CandidateId = idE,
        Name = "Candidate E",
        ImageUrl = string.Empty
    },
    new Candidate
    {
        CandidateId = idF,
        Name = "Candidate F",
        ImageUrl = string.Empty
    },
]);
        
db.Elections.Add(new Election
{
    ElectionId = Guid.NewGuid(),
    StartTime = DateTime.Today.AddDays(-1),
    EndTime = DateTime.Today.AddDays(1),
    CandidateIds = [idD, idE, idF]
        
});

db.SaveChanges();

app.Run();