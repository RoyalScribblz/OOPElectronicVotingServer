namespace OOPElectronicVotingServer.Contracts.ElectionContracts;

public class CreateElectionRequest
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<Guid> CandidateIds { get; set; } = default!;
}