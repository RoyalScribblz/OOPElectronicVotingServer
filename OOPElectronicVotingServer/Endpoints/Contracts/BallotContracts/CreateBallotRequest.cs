namespace OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;

public sealed class CreateBallotRequest
{
    public required Guid ElectionId { get; set; }
    public required string UserId { get; set; }
    public required Guid CandidateId { get; set; }
}