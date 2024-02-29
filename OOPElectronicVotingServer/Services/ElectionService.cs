using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;
using OOPElectronicVotingServer.Services.Abstractions;

namespace OOPElectronicVotingServer.Services;

public class ElectionService(VotingDatabase database) : IElectionService
{
    public async Task<Election?> CreateElection(CreateElectionRequest createRequest, CancellationToken cancellationToken)
    {
        Election election = new()
        {
            ElectionId = Guid.NewGuid(),
            Name = createRequest.Name,
            StartTime = createRequest.StartTime,
            EndTime = createRequest.EndTime,
            CandidateIds = createRequest.CandidateIds
        };

        try
        {
            await database.Elections.AddAsync(election, cancellationToken);

            await database.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return null;
        }

        return election;
    }
    
    public IEnumerable<GetElectionsResponse> GetElections()
    {
        return database.Elections
            .Select(databaseElection => new GetElectionsResponse
            {
                ElectionId = databaseElection.ElectionId,
                
                Name = databaseElection.Name,
                
                StartTime = databaseElection.StartTime,
                
                EndTime = databaseElection.EndTime,
                
                Candidates = database.Candidates
                    .Where(candidate => databaseElection.CandidateIds
                    .Contains(candidate.CandidateId))
                    .ToList(),
                
                VoteCount = database.Ballots
                    .Count(ballot => ballot.ElectionId == databaseElection.ElectionId)
            }).ToList();
    }
}