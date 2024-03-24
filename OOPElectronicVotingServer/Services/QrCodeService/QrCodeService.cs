using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Services.QrCodeService;

public sealed class QrCodeService(VotingDatabase database) : IQrCodeService
{
    public QrCode? GetQrCode(string qrCodeId) =>
        database.QrCodes.SingleOrDefault(qrCode => qrCode.QrCodeId == qrCodeId);
}