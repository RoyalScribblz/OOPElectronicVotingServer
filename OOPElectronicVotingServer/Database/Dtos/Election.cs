namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Election
{
    public Guid ElectionId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<Guid> CandidateIds { get; set; } = default!;
}