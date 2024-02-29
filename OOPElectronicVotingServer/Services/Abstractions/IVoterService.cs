using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Services.Abstractions;

public interface IVoterService
{
    Task<Voter?> CreateVoter(Voter voter, CancellationToken cancellationToken);

    Task<Voter?> GetVoter(string voterId, CancellationToken cancellationToken);
}