namespace OOPElectronicVotingServer.Database.Dtos;

public class QrCode
{
    public required string QrCodeId { get; set; }
    public required Guid ElectionId { get; set; }
}