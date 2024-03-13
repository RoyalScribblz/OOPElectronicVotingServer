using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

namespace OOPElectronicVotingServer.Services.ElectionService;

public interface IElectionService
{
    Task<Election?> CreateElection(CreateElectionRequest createRequest, CancellationToken cancellationToken);
    
    IEnumerable<GetElectionResponse> GetElections();

    GetElectionResponse? GetElection(Guid electionId);
}