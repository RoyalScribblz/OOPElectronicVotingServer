using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.CandidateEndpoints;

public static class GetCandidates
{
    public static void MapGetCandidates(this IEndpointRouteBuilder app) => app.MapGet("/candidates", Handler);

    private static async Task<IResult> Handler(Guid? electionId, VotingDatabase database, CancellationToken cancellationToken)
    {
        if (electionId == null)
        {
            return TypedResults.Ok(database.Candidates);
        }
        
        Election? election = database.Elections.FirstOrDefault(election => election.ElectionId == electionId);

        if (election == null)
        {
            return TypedResults.NotFound($"Election with id '{electionId}' does not exist.");
        }

        IQueryable<Candidate> candidates = database.Candidates.Where(candidate => election.CandidateIds.Contains(election.ElectionId));
        
        return TypedResults.Ok(candidates);
    }
}