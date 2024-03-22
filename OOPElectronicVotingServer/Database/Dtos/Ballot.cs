using OOPElectronicVotingServer.Models;

namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Ballot : BallotBase
{
    public required Guid BallotId { get; init; }
}