using OOPElectronicVotingServer.Contracts.BallotContracts;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.BallotEndpoints;

public static class CreateBallot
{
    public static void MapCreateBallot(this IEndpointRouteBuilder app) => app.MapPost("/ballot", Handler);

    private static async Task<IResult> Handler(CreateBallotRequest createRequest, VotingDatabase database, CancellationToken cancellationToken)
    {
        if (createRequest.Authentication != "password")
        {
            return TypedResults.Unauthorized();
        }

        Ballot ballot = new()
        {
            BallotId = Guid.NewGuid(),
            ElectionId = createRequest.ElectionId,
            VoterId = createRequest.VoterId,
            CandidateId = createRequest.CandidateId
        };
        
        await database.Ballots.AddAsync(ballot, cancellationToken);

        await database.SaveChangesAsync(cancellationToken);
        
        return TypedResults.Created("/ballots", ballot);
    }
}