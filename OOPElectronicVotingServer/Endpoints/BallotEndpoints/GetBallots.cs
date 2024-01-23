using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.BallotEndpoints;

public static class GetBallots
{
    public static void MapGetBallots(this IEndpointRouteBuilder app) => app.MapGet("/ballots", Handler);

    private static async Task<IResult> Handler(Guid electionId, VotingDatabase database, CancellationToken cancellationToken)
    {
        IQueryable<Ballot> ballots = database.Ballots.Where(ballot => ballot.ElectionId == electionId);
        return TypedResults.Ok(ballots);
    }
}