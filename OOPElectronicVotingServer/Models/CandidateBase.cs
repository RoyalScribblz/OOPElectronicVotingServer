namespace OOPElectronicVotingServer.Models;

public abstract record CandidateBase
{
    public required string Name { get; init; }
    public required string ImageUrl { get; init; }
    public required string Colour { get; init; }
}