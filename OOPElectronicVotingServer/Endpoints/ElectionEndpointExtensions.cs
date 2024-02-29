using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;
using OOPElectronicVotingServer.Services.Abstractions;

namespace OOPElectronicVotingServer.Endpoints;

public static class ElectionEndpointExtensions
{
    public static WebApplication MapElectionEndpoints(this WebApplication app)
    {
        app.MapPost("/election", async (CreateElectionRequest createRequest, IElectionService electionService, CancellationToken cancellationToken) =>
        {
            Election? election = await electionService.CreateElection(createRequest, cancellationToken);

            return election == null
                ? Results.BadRequest()
                : TypedResults.Created("/elections", election);
        });

        app.MapGet("/elections", (IElectionService electionService) =>
            TypedResults.Ok(electionService.GetElections()));

        return app;
    }  
}