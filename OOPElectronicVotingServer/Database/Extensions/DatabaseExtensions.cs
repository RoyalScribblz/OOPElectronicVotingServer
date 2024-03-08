using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Database.Extensions;

public static class DatabaseExtensions
{
    public static async Task Seed(this VotingDatabase database)
    {
        Guid idA = Guid.Parse("6ec3dda1-db7e-43cc-b295-a182683d7fb0");
        Guid idB = Guid.Parse("93da166e-1217-470e-8346-b3e871f672ad");
        Guid idC = Guid.Parse("6618f3ef-62f8-4eca-a33d-203c792e67af");
                
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
        
        Guid idD = Guid.Parse("bca7e44e-552b-42c0-b112-45bf52b43e94");
        Guid idE = Guid.Parse("8e389311-12f5-451d-9048-851ef6fabd93");
        Guid idF = Guid.Parse("494040ca-673a-4200-a6ab-831a87ab6641");
                
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
            ElectionId = Guid.Parse("483d63bf-1d12-477c-a643-5c28c7d5ae4e"),
            Name = "Another Election",
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = DateTime.Now.AddDays(1).AddHours(12),
            CandidateIds = [idD, idE, idF]
                
        });
        
        await database.Elections.AddAsync(new Election
        {
            ElectionId = Guid.Parse("e67f95e0-4250-4c75-8990-44a77fc35f22"),
            Name = "Finished election",
            StartTime = DateTime.Now.AddDays(-2),
            EndTime = DateTime.Now.AddDays(-1),
            CandidateIds = [idA, idE, idF]
                
        });

        await database.QrCodes.AddRangeAsync([
            new QrCode
            {
                QrCodeId = "voteWithQR|B8SX9Pem6cuYitrFzKunXcB23Sk3xbtTB3nSzx5bjqpTSmQ5Cre42xjSzjvrYaRF",
                ElectionId = Guid.Parse("e67f95e0-4250-4c75-8990-44a77fc35f22")
            },
            new QrCode
            {
                QrCodeId = "voteWithQR|p3RNi7ckhSkc59KvuLedwxACL2v4W7B2gS0pVQ7HgeBAYE8a8gpZWLQhVFuf5C3h",
                ElectionId = Guid.Parse("e67f95e0-4250-4c75-8990-44a77fc35f22")
            },
            new QrCode
            {
                QrCodeId = "voteWithQR|u7Q4RM3MKfHtAwKN9TXZGETXFaf1ApYEFc45B6MkknivKRnr0p5ex9PChG13U06X",
                ElectionId = Guid.Parse("e67f95e0-4250-4c75-8990-44a77fc35f22")
            },
            new QrCode
            {
                QrCodeId = "voteWithQR|5qaccjKGcecjkfMtWMEZKddQNccSySrK7eHP2x5izTuBLM7iN5Sby0AJzewUMQYy",
                ElectionId = Guid.Parse("e67f95e0-4250-4c75-8990-44a77fc35f22")
            },new QrCode
            {
                QrCodeId = "voteWithQR|DVv3B2Lb2DhKaCUhCzX76wdS449XcAZpFjzgvy9mEk40YfgfRwaFKQyqBY80uVXB",
                ElectionId = Guid.Parse("e67f95e0-4250-4c75-8990-44a77fc35f22")
            }
        ]);
        
        await database.SaveChangesAsync();
    }
}