using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Endpoints.ElectionEndpoints.Contracts;

namespace OOPElectronicVotingServer.Endpoints.ElectionEndpoints;

public static class GetElections
{
    public static void MapGetElections(this IEndpointRouteBuilder app) => app.MapGet("/elections", Handler);

    private static Task<IResult> Handler(VotingDatabase database, CancellationToken cancellationToken)
    {
        List<GetElectionsResponse> elections = database.Elections
            .Select(databaseElection => new GetElectionsResponse
            {
                ElectionId = databaseElection.ElectionId,
                StartTime = databaseElection.StartTime,
                EndTime = databaseElection.EndTime,
                Candidates = database.Candidates.Where(
                    candidate => databaseElection.CandidateIds.Contains(candidate.CandidateId)).ToList()
            })
            .ToList();

        return Task.FromResult<IResult>(TypedResults.Ok(elections));
    }
}