namespace OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

public sealed class CreateCandidateRequest
{
    public required string Name { get; set; }
    public required string ImageUrl { get; set; }
    public required string Colour { get; set; }
}