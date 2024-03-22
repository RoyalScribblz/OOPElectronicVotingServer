using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

namespace OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

public sealed record GetElectionResponse
{
    public required Guid ElectionId { get; init; }
    public required string Name { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required IEnumerable<CandidateWithVoteCount> Candidates { get; init; }
}