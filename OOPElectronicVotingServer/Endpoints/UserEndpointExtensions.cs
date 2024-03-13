using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Services.Abstractions;

namespace OOPElectronicVotingServer.Endpoints;

public static class UserEndpointExtensions
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/user", async (User user, IUserService userService, CancellationToken cancellationToken) =>
            await userService.CreateUser(user, cancellationToken) == null
                ? Results.BadRequest()
                : TypedResults.Created($"/user/{user.UserId}"));

        app.MapGet("/user/{userId}", async (string userId, IUserService userService, CancellationToken cancellationToken) =>
        {
            User? user = await userService.GetUser(userId, cancellationToken);

            return user == null ? Results.NotFound() : TypedResults.Ok(user);
        });

        return app;
    }  
}