using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Endpoints.ElectionEndpoints.Contracts;

public class GetElectionsResponse
{
    public required Guid ElectionId { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required List<Candidate> Candidates { get; set; }
}