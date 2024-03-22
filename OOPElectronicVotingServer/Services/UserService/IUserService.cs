using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Services.UserService;

public interface IUserService
{
    /// <summary>
    /// Create a user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The created user or null if it wasn't created.</returns>
    Task<User?> CreateUser(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Get a specific user.
    /// </summary>
    /// <param name="userId">ID of the user to get.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The user or null if it wasn't found.</returns>
    Task<User?> GetUser(string userId, CancellationToken cancellationToken);
}