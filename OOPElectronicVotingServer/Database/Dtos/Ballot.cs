namespace OOPElectronicVotingServer.Database.Dtos;

public sealed class Ballot
{
    public required Guid BallotId { set; get; }
    public required Guid ElectionId { set; get; }
    public required string VoterId { set; get; }
    public required Guid CandidateId { set; get; }
}