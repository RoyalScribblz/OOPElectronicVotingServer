namespace OOPElectronicVotingServer.Models;

public abstract record BallotBase
{
    public required Guid ElectionId { get; init; }
    public required string UserId { get; init; }
    public required Guid CandidateId { get; init; }
}