using OOPElectronicVotingServer.Models;

namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record User : UserBase
{
    public required string UserId { get; init; }
    public required string Email { get; init; }
}