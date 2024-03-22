using OOPElectronicVotingServer.Models;

namespace OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

public sealed record CandidateWithVoteCount : CandidateBase
{
    public required Guid CandidateId { get; init; }
    public required int VoteCount { get; init; }
}