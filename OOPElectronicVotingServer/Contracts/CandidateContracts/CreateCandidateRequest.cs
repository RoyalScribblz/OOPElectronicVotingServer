namespace OOPElectronicVotingServer.Contracts.CandidateContracts;

public class CreateCandidateRequest
{
    public required string Name { get; set; }
    public required string ImageUrl { get; set; }
}