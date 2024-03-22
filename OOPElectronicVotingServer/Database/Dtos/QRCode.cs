namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record QrCode
{
    public required string QrCodeId { get; init; }
    public required Guid ElectionId { get; init; }
}