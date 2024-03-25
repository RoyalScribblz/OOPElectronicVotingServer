using Microsoft.EntityFrameworkCore;
using OOPElectronicVotingServer.Database;
using OOPElectronicVotingServer.Database.Dtos;

namespace OOPElectronicVotingServer.Services.UserService;

public sealed class UserService(VotingDatabase database) : IUserService
{
    public async Task<User?> CreateUser(User user, CancellationToken cancellationToken)
    {
        try
        {
            await database.Users.AddAsync(user, cancellationToken);

            await database.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return null;
        }

        return user;
    }

    public Task<User?> GetUser(string userId, CancellationToken cancellationToken) =>
        database.Users.SingleOrDefaultAsync(user => user.UserId == userId, cancellationToken: cancellationToken);
}