namespace OOPElectronicVotingServer.Models;

public abstract record ElectionBase
{
    public required string Name { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required IEnumerable<Guid> CandidateIds { get; init; }
}