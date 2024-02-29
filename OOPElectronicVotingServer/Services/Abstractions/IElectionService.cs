using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

namespace OOPElectronicVotingServer.Services.Abstractions;

public interface IElectionService
{
    Task<Election?> CreateElection(CreateElectionRequest createRequest, CancellationToken cancellationToken);
    
    IEnumerable<GetElectionsResponse> GetElections();
}