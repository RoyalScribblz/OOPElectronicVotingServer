namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Election
{
    public required Guid ElectionId { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required List<Guid> CandidateIds { get; set; }
}