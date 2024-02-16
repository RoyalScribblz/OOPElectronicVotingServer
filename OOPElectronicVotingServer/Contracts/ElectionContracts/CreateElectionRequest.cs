namespace OOPElectronicVotingServer.Contracts.ElectionContracts;

public class CreateElectionRequest
{
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required List<Guid> CandidateIds { get; set; }
}