using OOPElectronicVotingServer.Contracts.VoterContracts;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.VoterEndpoints;

public static class CreateVoter
{
    public static void MapCreateVoter(this IEndpointRouteBuilder app) => app.MapPost("/voters", Handler);

    private static async Task<IResult> Handler(CreateVoterRequest createRequest, VotingDatabase database, CancellationToken cancellationToken)
    {
        Voter voter = new()
        {
            VoterId = Guid.NewGuid(),
            NationalId = createRequest.NationalId,
            FirstName = createRequest.FirstName,
            LastName = createRequest.LastName,
            MiddleName = createRequest.MiddleName,
            DateOfBirth = createRequest.DateOfBirth,
            Address = createRequest.Address,
            Postcode = createRequest.Postcode,
            Country = createRequest.Country,
            Email = createRequest.Email,
            PhoneNumber = createRequest.PhoneNumber,
            PasswordHash = createRequest.PasswordHash
        };
        
        await database.Voters.AddAsync(voter, cancellationToken);

        await database.SaveChangesAsync(cancellationToken);

        return TypedResults.Created($"/voters/{voter.VoterId}", voter);
    }
}