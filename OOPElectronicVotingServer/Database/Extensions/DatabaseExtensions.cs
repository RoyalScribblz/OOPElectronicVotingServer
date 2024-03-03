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
            ElectionId = Guid.Parse("dc6703e2-589d-42bb-aaa6-30cc09d27822"),
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

        await database.QrCodes.AddRangeAsync([
            new QrCode
            {
                QrCodeId = "voteWithQR|B8SX9Pem6cuYitrFzKunXcB23Sk3xbtTB3nSzx5bjqpTSmQ5Cre42xjSzjvrYaRF"
            },
            new QrCode
            {
                QrCodeId = "voteWithQR|p3RNi7ckhSkc59KvuLedwxACL2v4W7B2gS0pVQ7HgeBAYE8a8gpZWLQhVFuf5C3h"
            },
            new QrCode
            {
                QrCodeId = "voteWithQR|u7Q4RM3MKfHtAwKN9TXZGETXFaf1ApYEFc45B6MkknivKRnr0p5ex9PChG13U06X"
            },
            new QrCode
            {
                QrCodeId = "voteWithQR|5qaccjKGcecjkfMtWMEZKddQNccSySrK7eHP2x5izTuBLM7iN5Sby0AJzewUMQYy"
            },new QrCode
            {
                QrCodeId = "voteWithQR|DVv3B2Lb2DhKaCUhCzX76wdS449XcAZpFjzgvy9mEk40YfgfRwaFKQyqBY80uVXB"
            }
        ]);
        
        await database.SaveChangesAsync();
    }
}