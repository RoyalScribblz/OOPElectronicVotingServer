using OOPElectronicVotingServer.Database;

namespace OOPElectronicVotingServer.Endpoints.ElectionEndpoints;

public static class GetElections
{
    public static void MapGetElections(this IEndpointRouteBuilder app) => app.MapGet("/elections", Handler);

    private static Task<IResult> Handler(VotingDatabase database, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(TypedResults.Ok(database.Elections));
    }
}