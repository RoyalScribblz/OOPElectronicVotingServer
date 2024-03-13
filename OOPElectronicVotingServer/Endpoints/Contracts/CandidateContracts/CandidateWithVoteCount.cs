namespace OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

public sealed class CandidateWithVoteCount
{
    public required Guid CandidateId { get; set; }
    public required string Name { get; set; }
    public required string ImageUrl { get; set; }
    public required string Colour { get; set; }
    public required int VoteCount { get; set; }
}