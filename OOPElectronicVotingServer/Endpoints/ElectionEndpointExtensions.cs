using Microsoft.AspNetCore.Authorization;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;
using OOPElectronicVotingServer.Services.ElectionService;

namespace OOPElectronicVotingServer.Endpoints;

public static class ElectionEndpointExtensions
{
    public static WebApplication MapElectionEndpoints(this WebApplication app)
    {
        app.MapPost("/election", [Authorize] async (CreateElectionRequest createRequest, IElectionService electionService, CancellationToken cancellationToken) =>
        {
            Election? election = await electionService.CreateElection(createRequest, cancellationToken);

            return election == null
                ? Results.BadRequest()
                : TypedResults.Created($"/election/{election.ElectionId}", election);
        });

        app.MapGet("/elections", (IElectionService electionService) =>
            TypedResults.Ok(electionService.GetElections()));

        app.MapGet("/election/{electionId:guid}", (IElectionService electionService, Guid electionId) =>
        {
            GetElectionResponse? election = electionService.GetElection(electionId);

            return election == null
                ? Results.NotFound()
                : TypedResults.Ok(election);
        });

        return app;
    }  
}