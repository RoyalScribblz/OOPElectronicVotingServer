using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

namespace OOPElectronicVotingServer.Services.Abstractions;

public interface ICandidateService
{
    Task<Candidate?> CreateCandidate(CreateCandidateRequest createRequest, CancellationToken cancellationToken);

    IEnumerable<Candidate> GetCandidates();
}