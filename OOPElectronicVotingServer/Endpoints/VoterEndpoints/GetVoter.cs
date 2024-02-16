using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.VoterEndpoints;

public static class GetVoter
{
    public static void MapGetVoter(this IEndpointRouteBuilder app) => app.MapGet("/voters/{voterId}", Handler);

    private static IResult Handler(string voterId, VotingDatabase database, CancellationToken cancellationToken)
    {
        Voter? voter = database.Voters.FirstOrDefault(voter => voter.VoterId == voterId);

        return voter == null ? TypedResults.NotFound() : TypedResults.Ok(voter);
    }
}