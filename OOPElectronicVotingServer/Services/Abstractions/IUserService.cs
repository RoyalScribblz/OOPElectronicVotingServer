using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Services.Abstractions;

public interface IUserService
{
    Task<User?> CreateUser(User user, CancellationToken cancellationToken);

    Task<User?> GetUser(string userId, CancellationToken cancellationToken);
}