namespace OOPElectronicVotingServer.Contracts.CandidateContracts;

public class CreateCandidateRequest
{
    public string Name { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}