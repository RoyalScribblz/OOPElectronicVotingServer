using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

namespace OOPElectronicVotingServer.Services.ElectionService;

public sealed class ElectionService(VotingDatabase database) : IElectionService
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
    
    public IEnumerable<GetElectionResponse> GetElections() => database.Elections
        .Select(election => new GetElectionResponse
        {
            ElectionId = election.ElectionId,

            Name = election.Name,

            StartTime = election.StartTime,

            EndTime = election.EndTime,

            Candidates = database.Candidates
                .Select(candidate => new CandidateWithVoteCount
                {
                    CandidateId = candidate.CandidateId,
                    Name = candidate.Name,
                    ImageUrl = candidate.ImageUrl,
                    Colour = candidate.Colour,
                    VoteCount = election.EndTime < DateTime.Now
                        ? database.Ballots.Count(ballot => ballot.ElectionId == election.ElectionId 
                                                           && ballot.CandidateId == candidate.CandidateId)
                        : 0
                })
                .Where(candidate => election.CandidateIds.Contains(candidate.CandidateId))
                .ToList()
        }).ToList();

    public GetElectionResponse? GetElection(Guid electionId) => database.Elections.Select(election => new GetElectionResponse
    {
        ElectionId = election.ElectionId,

        Name = election.Name,

        StartTime = election.StartTime,

        EndTime = election.EndTime,

        Candidates = database.Candidates
            .Select(candidate => new CandidateWithVoteCount
            {
                CandidateId = candidate.CandidateId,
                Name = candidate.Name,
                ImageUrl = candidate.ImageUrl,
                Colour = candidate.Colour,
                VoteCount = election.EndTime < DateTime.Now
                    ? database.Ballots.Count(ballot => ballot.ElectionId == election.ElectionId 
                                                       && ballot.CandidateId == candidate.CandidateId)
                    : 0
            })
            .Where(candidate => election.CandidateIds.Contains(candidate.CandidateId))
            .ToList()
    }).SingleOrDefault(election => election.ElectionId == electionId);
}