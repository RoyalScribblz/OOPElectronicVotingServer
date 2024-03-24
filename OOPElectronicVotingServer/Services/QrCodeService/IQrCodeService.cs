using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Services.QrCodeService;

public interface IQrCodeService
{
    /// <summary>
    /// Get a specific qr code.
    /// </summary>
    /// <param name="qrCodeId">ID of the qr code.</param>
    /// <returns>A qr code or null if it doesn't exist.</returns>
    QrCode? GetQrCode(string qrCodeId);
}