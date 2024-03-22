using OOPElectronicVotingServer.BaseModels;

namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Ballot : BallotBase
{
    public required Guid BallotId { get; init; }
}