namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Candidate
{
    public Guid CandidateId { get; set; }
    public string Name { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}