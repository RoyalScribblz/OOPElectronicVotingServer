using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

public class GetElectionsResponse
{
    public required Guid ElectionId { get; set; }
    public required string Name { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required List<Candidate> Candidates { get; set; }
    public required int VoteCount { get; set; }
}