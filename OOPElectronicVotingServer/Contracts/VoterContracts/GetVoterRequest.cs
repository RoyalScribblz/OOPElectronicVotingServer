namespace OOPElectronicVotingServer.Contracts.VoterContracts;

public class GetVoterRequest
{
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}