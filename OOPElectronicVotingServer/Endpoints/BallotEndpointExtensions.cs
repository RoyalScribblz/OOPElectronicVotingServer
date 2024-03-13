using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;
using OOPElectronicVotingServer.Services.BallotService;

namespace OOPElectronicVotingServer.Endpoints;

public static class BallotEndpointExtensions
{
    public static WebApplication MapBallotEndpoints(this WebApplication app)
    {
        app.MapPost("/ballot", async (CreateBallotRequest createRequest, IBallotService ballotService, CancellationToken cancellationToken) =>
        {
            Ballot? ballot = await ballotService.CreateBallot(createRequest, cancellationToken);
    
            return ballot == null
                ? Results.BadRequest()
                : TypedResults.Created("/ballots", ballot);
        });

        app.MapGet("/ballots", (Guid electionId, IBallotService ballotService, CancellationToken cancellationToken) =>
            TypedResults.Ok(ballotService.GetBallots(electionId, cancellationToken)));

        return app;
    }
}