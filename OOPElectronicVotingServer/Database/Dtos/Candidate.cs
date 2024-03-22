using OOPElectronicVotingServer.Models;

namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Candidate : CandidateBase
{
    public required Guid CandidateId { get; init; }
}