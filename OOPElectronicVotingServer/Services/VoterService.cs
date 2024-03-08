using Microsoft.EntityFrameworkCore;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Services.Abstractions;

namespace OOPElectronicVotingServer.Services;

public sealed class VoterService(VotingDatabase database) : IVoterService
{
    public async Task<Voter?> CreateVoter(Voter voter, CancellationToken cancellationToken)
    {
        try
        {
            await database.Voters.AddAsync(voter, cancellationToken);

            await database.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return null;
        }

        return voter;
    }

    public async Task<Voter?> GetVoter(string voterId, CancellationToken cancellationToken) =>
        await database.Voters.FirstOrDefaultAsync(voter => voter.VoterId == voterId, cancellationToken: cancellationToken);
}