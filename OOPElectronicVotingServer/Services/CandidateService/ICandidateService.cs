using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

namespace OOPElectronicVotingServer.Services.CandidateService;

public interface ICandidateService
{
    Task<Candidate?> CreateCandidate(CreateCandidateRequest createRequest, CancellationToken cancellationToken);

    IEnumerable<Candidate> GetCandidates();
}