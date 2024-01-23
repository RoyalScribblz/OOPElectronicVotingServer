namespace OOPElectronicVotingServer.Database.Dtos;

public sealed class Ballot
{
    public Guid BallotId { set; get; }
    public Guid ElectionId { set; get; }
    public Guid VoterId { set; get; }
    public Guid CandidateId { set; get; }
}