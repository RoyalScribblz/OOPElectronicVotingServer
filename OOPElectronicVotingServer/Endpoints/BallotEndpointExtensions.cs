using Microsoft.AspNetCore.Http.HttpResults;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;
using OOPElectronicVotingServer.Services.BallotService;

namespace OOPElectronicVotingServer.Endpoints;

public static class BallotEndpointExtensions
{
    public static WebApplication MapBallotEndpoints(this WebApplication app)
    {
        app.MapPost("/ballot", async Task<Results<BadRequest, Created<Ballot>>> (
            CreateBallotRequest createRequest,
            IBallotService ballotService,
            CancellationToken cancellationToken) =>
        {
            Ballot? ballot = await ballotService.CreateBallot(createRequest, cancellationToken);
    
            return ballot == null
                ? TypedResults.BadRequest()
                : TypedResults.Created("/ballots", ballot);
        }).RequireAuthorization().WithTags("Ballot");

        app.MapGet("/ballots", (Guid electionId, IBallotService ballotService) =>
            TypedResults.Ok(ballotService.GetBallots(electionId))).WithTags("Ballot");

        return app;
    }
}