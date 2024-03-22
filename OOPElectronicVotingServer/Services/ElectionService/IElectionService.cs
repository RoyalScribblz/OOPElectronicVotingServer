using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

namespace OOPElectronicVotingServer.Services.ElectionService;

public interface IElectionService
{
    /// <summary>
    /// Create an election.
    /// </summary>
    /// <param name="createRequest">Values to create the election with.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The created election or null if it wasn't created.</returns>
    Task<Election?> CreateElection(CreateElectionRequest createRequest, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get all elections.
    /// </summary>
    /// <returns>An IEnumerable of elections with vote count on the candidates.</returns>
    IEnumerable<GetElectionResponse> GetElections();

    /// <summary>
    /// Get a specific election.
    /// </summary>
    /// <param name="electionId">ID of the election.</param>
    /// <returns>An elections with vote count on the candidates.</returns>
    GetElectionResponse? GetElection(Guid electionId);
}