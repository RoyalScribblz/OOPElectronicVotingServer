using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;
using OOPElectronicVotingServer.Services.CandidateService;

namespace OOPElectronicVotingServer.Endpoints;

public static class CandidateEndpointExtensions
{
    public static WebApplication MapCandidateEndpoints(this WebApplication app)
    {
        app.MapPost("/candidate", async (CreateCandidateRequest createRequest,  ICandidateService candidateService, CancellationToken cancellationToken) =>
        {
            Candidate? candidate = await candidateService.CreateCandidate(createRequest, cancellationToken);
    
            return candidate == null
                ? Results.BadRequest()
                : TypedResults.Created("/candidates", candidate);
        });

        app.MapGet("/candidates", (ICandidateService candidateService) =>
            TypedResults.Ok(candidateService.GetCandidates()));

        return app;
    }    
}