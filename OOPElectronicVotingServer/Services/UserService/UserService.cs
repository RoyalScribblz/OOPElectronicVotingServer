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

    public async Task<User?> GetUser(string userId, CancellationToken cancellationToken) =>
        await database.Users.FirstOrDefaultAsync(user => user.UserId == userId, cancellationToken: cancellationToken);

    public async Task<bool> IsEmpty() => !await database.Users.AnyAsync();
}