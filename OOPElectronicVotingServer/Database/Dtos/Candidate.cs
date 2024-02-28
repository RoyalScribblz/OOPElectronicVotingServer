namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Candidate
{
    public required Guid CandidateId { get; set; }
    public required string Name { get; set; }
    public required string ImageUrl { get; set; }
    public required string Colour { get; set; }
}