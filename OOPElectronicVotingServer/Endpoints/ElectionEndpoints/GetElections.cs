using OOPElectronicVotingServer.Contracts.ElectionContracts;
using OOPElectronicVotingServer.Database;

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
                Name = databaseElection.Name,
                StartTime = databaseElection.StartTime,
                EndTime = databaseElection.EndTime,
                Candidates = database.Candidates.Where(
                    candidate => databaseElection.CandidateIds.Contains(candidate.CandidateId)).ToList()
            })
            .ToList();

        return Task.FromResult<IResult>(TypedResults.Ok(elections));
    }
}