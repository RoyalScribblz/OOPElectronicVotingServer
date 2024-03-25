using Microsoft.EntityFrameworkCore;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Services.QrCodeService;

public sealed class QrCodeService(VotingDatabase database) : IQrCodeService
{
    public Task<QrCode?> GetQrCode(string qrCodeId) =>
        database.QrCodes.SingleOrDefaultAsync(qrCode => qrCode.QrCodeId == qrCodeId);
}