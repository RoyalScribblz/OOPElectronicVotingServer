using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;

namespace OOPElectronicVotingServer.Services.BallotService;

public sealed class BallotService(VotingDatabase database) : IBallotService
{
    public async Task<Ballot?> CreateBallot(CreateBallotRequest createRequest, CancellationToken cancellationToken)
    {
        Ballot ballot = new()
        {
            BallotId = Guid.NewGuid(),
            ElectionId = createRequest.ElectionId,
            UserId = createRequest.UserId,
            CandidateId = createRequest.CandidateId
        };

        try
        {
            await database.Ballots.AddAsync(ballot, cancellationToken);

            await database.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return null;
        }

        return ballot;
    }

    public IEnumerable<Ballot> GetBallots(Guid electionId, CancellationToken cancellationToken) =>
        database.Ballots.Where(ballot => ballot.ElectionId == electionId);
}