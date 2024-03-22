using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;

namespace OOPElectronicVotingServer.Services.BallotService;

public interface IBallotService
{
    /// <summary>
    /// Create a ballot.
    /// </summary>
    /// <param name="createRequest">Values to create the ballot with.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The created ballot or null if it wasn't created.</returns>
    Task<Ballot?> CreateBallot(CreateBallotRequest createRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Get all ballots for a given election.
    /// </summary>
    /// <param name="electionId">Election ID associated with the ballot.</param>
    /// <returns>An IEnumerable of ballots associated with the election.</returns>
    IEnumerable<Ballot> GetBallots(Guid electionId);
}