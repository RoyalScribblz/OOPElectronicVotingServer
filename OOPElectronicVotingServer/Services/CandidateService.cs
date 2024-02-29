using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;
using OOPElectronicVotingServer.Services.Abstractions;

namespace OOPElectronicVotingServer.Services;

public class CandidateService(VotingDatabase database) : ICandidateService
{
    public async Task<Candidate?> CreateCandidate(CreateCandidateRequest createRequest, CancellationToken cancellationToken)
    {
        Candidate candidate = new()
        {
            CandidateId = Guid.NewGuid(),
            Name = createRequest.Name,
            ImageUrl = createRequest.ImageUrl,
            Colour = createRequest.Colour
        };

        try
        {
            await database.Candidates.AddAsync(candidate, cancellationToken);

            await database.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return null;
        }

        return candidate;
    }

    public IEnumerable<Candidate> GetCandidates() => database.Candidates;
}