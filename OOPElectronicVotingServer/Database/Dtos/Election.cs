using OOPElectronicVotingServer.Models;

namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Election : ElectionBase
{
    public required Guid ElectionId { get; init; }
}