using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.VoterEndpoints;

public static class CreateVoter
{
    public static void MapCreateVoter(this IEndpointRouteBuilder app) => app.MapPost("/voter", Handler);

    private static async Task<IResult> Handler(Voter voter, VotingDatabase database, CancellationToken cancellationToken)
    {
        await database.Voters.AddAsync(voter, cancellationToken);

        await database.SaveChangesAsync(cancellationToken);

        return TypedResults.Created($"/voters/{voter.VoterId}");
    }
}