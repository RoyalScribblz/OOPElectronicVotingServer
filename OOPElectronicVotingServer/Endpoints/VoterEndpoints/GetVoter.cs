using OOPElectronicVotingServer.Contracts.VoterContracts;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.VoterEndpoints;

public static class GetVoter
{
    public static void MapGetVoter(this IEndpointRouteBuilder app) => app.MapPost("/voters/{voterId:guid}", Handler);

    private static async Task<IResult> Handler(Guid voterId, GetVoterRequest voterRequest, VotingDatabase database, CancellationToken cancellationToken)
    {
        Voter? voter = database.Voters.FirstOrDefault(voter => voter.VoterId == voterId);

        if (voter == null)
        {
            return TypedResults.NotFound();
        }

        if (voter.Email != voterRequest.Email || voter.PasswordHash != voterRequest.PasswordHash)
        {
            return TypedResults.Unauthorized();
        }
        
        return TypedResults.Ok(voter);
    }
}