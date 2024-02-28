using OOPElectronicVotingServer.Contracts.CandidateContracts;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.CandidateEndpoints;

public static class CreateCandidate
{
    public static void MapCreateCandidate(this IEndpointRouteBuilder app) => app.MapPost("/candidate", Handler);

    private static async Task<IResult> Handler(CreateCandidateRequest createRequest, VotingDatabase database, CancellationToken cancellationToken)
    {
        Candidate candidate = new()
        {
            CandidateId = Guid.NewGuid(),
            Name = createRequest.Name,
            ImageUrl = createRequest.ImageUrl,
            Colour = createRequest.Colour
        };

        await database.Candidates.AddAsync(candidate, cancellationToken);

        await database.SaveChangesAsync(cancellationToken);

        return TypedResults.Created("/candidates", candidate);
    }
}