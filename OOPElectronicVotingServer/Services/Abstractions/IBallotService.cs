using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;

namespace OOPElectronicVotingServer.Services.Abstractions;

public interface IBallotService
{
    Task<Ballot?> CreateBallot(CreateBallotRequest createRequest, CancellationToken cancellationToken);

    IEnumerable<Ballot> GetBallots(Guid electionId, CancellationToken cancellationToken);
}