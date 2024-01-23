using OOPElectronicVotingServer.Contracts.ElectionContracts;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.ElectionEndpoints;

public static class CreateElection
{
    public static void MapCreateElection(this IEndpointRouteBuilder app) => app.MapPost("/elections", Handler);

    private static async Task<IResult> Handler(CreateElectionRequest createRequest, VotingDatabase database, CancellationToken cancellationToken)
    {
        Election election = new()
        {
            ElectionId = Guid.NewGuid(),
            StartTime = createRequest.StartTime,
            EndTime = createRequest.EndTime,
            CandidateIds = createRequest.CandidateIds
        };

        await database.Elections.AddAsync(election, cancellationToken);

        await database.SaveChangesAsync(cancellationToken);

        return TypedResults.Created("/elections", election);
    }
}