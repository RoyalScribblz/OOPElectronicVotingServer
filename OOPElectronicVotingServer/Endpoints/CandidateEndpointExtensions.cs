using Microsoft.AspNetCore.Http.HttpResults;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;
using OOPElectronicVotingServer.Extensions;
using OOPElectronicVotingServer.Services.CandidateService;

namespace OOPElectronicVotingServer.Endpoints;

public static class CandidateEndpointExtensions
{
    public static WebApplication MapCandidateEndpoints(this WebApplication app)
    {
        app.MapPost("/candidate", async Task<Results<UnauthorizedHttpResult, BadRequest, Created<Candidate>>>(
            CreateCandidateRequest createRequest, 
            ICandidateService candidateService,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            if (context.User.IsAdmin() == false)
            {
                return TypedResults.Unauthorized();
            }
            
            Candidate? candidate = await candidateService.CreateCandidate(createRequest, cancellationToken);
    
            return candidate == null
                ? TypedResults.BadRequest()
                : TypedResults.Created("/candidates", candidate);
        }).RequireAuthorization().WithTags("Candidate");

        app.MapGet("/candidates", (ICandidateService candidateService) =>
            TypedResults.Ok(candidateService.GetCandidates())).WithTags("Candidate");

        return app;
    }    
}