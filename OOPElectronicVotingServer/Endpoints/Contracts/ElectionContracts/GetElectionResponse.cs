using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

namespace OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

public class GetElectionResponse
{
    public required Guid ElectionId { get; set; }
    public required string Name { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required List<CandidateWithVoteCount> Candidates { get; set; }
}