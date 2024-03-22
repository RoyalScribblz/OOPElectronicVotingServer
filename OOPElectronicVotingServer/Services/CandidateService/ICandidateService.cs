using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

namespace OOPElectronicVotingServer.Services.CandidateService;

public interface ICandidateService
{
    /// <summary>
    /// Create a candidate.
    /// </summary>
    /// <param name="createRequest">Values to create the candidate with.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The created candidate or null if it wasn't created.</returns>
    Task<Candidate?> CreateCandidate(CreateCandidateRequest createRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Get all candidates.
    /// </summary>
    /// <returns>An IEnumerable of candidates.</returns>
    IEnumerable<Candidate> GetCandidates();
}