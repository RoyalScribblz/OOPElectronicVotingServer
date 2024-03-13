using Microsoft.EntityFrameworkCore;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Database;

public class VotingDatabase : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("TestDb");
    }

    public required DbSet<User> Users { get; init; }
    
    public required DbSet<Candidate> Candidates { get; init; }
    
    public required DbSet<Election> Elections { get; init; }

    public required DbSet<Ballot> Ballots { get; init; }
    
    public required DbSet<QrCode> QrCodes { get; init; }
}