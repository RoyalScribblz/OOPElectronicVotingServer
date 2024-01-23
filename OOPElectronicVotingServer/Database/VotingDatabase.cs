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

    public DbSet<Voter> Voters { get; init; } = default!;
    
    public DbSet<Candidate> Candidates { get; init; } = default!;
    
    public DbSet<Election> Elections { get; init; } = default!;

    public DbSet<Ballot> Ballots { get; init; } = default!;
}