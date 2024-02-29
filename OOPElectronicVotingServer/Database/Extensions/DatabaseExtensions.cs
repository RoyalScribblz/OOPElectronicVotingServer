using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Database.Extensions;

public static class DatabaseExtensions
{
    public static async Task Seed(this VotingDatabase database)
    {
        Guid idA = Guid.NewGuid(); 
        Guid idB = Guid.NewGuid(); 
        Guid idC = Guid.NewGuid(); 
                
        await database.Candidates.AddRangeAsync([
            new Candidate
            {
                CandidateId = idA,
                Name = "Candidate A",
                ImageUrl = string.Empty,
                Colour = "2176ff"
            },
            new Candidate
            {
                CandidateId = idB,
                Name = "Candidate B",
                ImageUrl = string.Empty,
                Colour = "ff21cf"
            },
            new Candidate
            {
                CandidateId = idC,
                Name = "Candidate C",
                ImageUrl = string.Empty,
                Colour = "ff2121"
            },
        ]);
                
        await database.Elections.AddAsync(new Election
        {
            ElectionId = Guid.NewGuid(),
            Name = "Really Cool Election",
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = DateTime.Now.AddDays(1),
            CandidateIds = [idA, idB, idC]
                
        });
        
        Guid idD = Guid.NewGuid(); 
        Guid idE = Guid.NewGuid(); 
        Guid idF = Guid.NewGuid(); 
                
        await database.Candidates.AddRangeAsync([
            new Candidate
            {
                CandidateId = idD,
                Name = "Candidate D",
                ImageUrl = string.Empty,
                Colour = "21ff25"
            },
            new Candidate
            {
                CandidateId = idE,
                Name = "Candidate E",
                ImageUrl = string.Empty,
                Colour = "c2fa19"
            },
            new Candidate
            {
                CandidateId = idF,
                Name = "Candidate F",
                ImageUrl = string.Empty,
                Colour = "fa5c19"
            },
        ]);
                
        await database.Elections.AddAsync(new Election
        {
            ElectionId = Guid.NewGuid(),
            Name = "Another Election",
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = DateTime.Now.AddDays(1).AddHours(12),
            CandidateIds = [idD, idE, idF]
                
        });
        
        await database.Elections.AddAsync(new Election
        {
            ElectionId = Guid.NewGuid(),
            Name = "Finished election",
            StartTime = DateTime.Now.AddDays(-2),
            EndTime = DateTime.Now.AddDays(-1),
            CandidateIds = [idA, idE, idF]
                
        });
        
        await database.SaveChangesAsync();
    }
}