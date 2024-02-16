namespace OOPElectronicVotingServer.Database.Dtos;

public sealed record Voter
{
    public required string VoterId { get; set; }
    public required string NationalId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string Address { get; set; }
    public required string Postcode { get; set; }
    public required string Country { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}