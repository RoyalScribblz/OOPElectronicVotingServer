namespace OOPElectronicVotingServer.Contracts.BallotContracts;

public sealed class CreateBallotRequest
{
    public string Authentication { get; set; } = default!;
    public Guid ElectionId { get; set; }
    public Guid VoterId { get; set; }
    public Guid CandidateId { get; set; }
}