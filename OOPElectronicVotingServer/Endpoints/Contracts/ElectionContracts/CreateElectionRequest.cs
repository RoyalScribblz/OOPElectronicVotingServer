namespace OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

public class CreateElectionRequest
{
    public required string Name { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required List<Guid> CandidateIds { get; set; }
}