namespace OOPElectronicVotingServer.Models;

public abstract record UserBase
{
    public required string NationalId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string MiddleName { get; init; }
    public required DateTime DateOfBirth { get; init; }
    public required string Address { get; init; }
    public required string Postcode { get; init; }
    public required string Country { get; init; }
    public required string PhoneNumber { get; init; }
}