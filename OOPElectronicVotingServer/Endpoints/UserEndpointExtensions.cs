using Microsoft.AspNetCore.Authorization;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.UserContracts;
using OOPElectronicVotingServer.Services.UserService;

namespace OOPElectronicVotingServer.Endpoints;

public static class UserEndpointExtensions
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/user", [Authorize] async (CreateUserRequest createRequest, IUserService userService, CancellationToken cancellationToken) =>
        {
            User user = new User
            {
                UserId = createRequest.UserId,
                NationalId = createRequest.NationalId,
                FirstName = createRequest.FirstName,
                LastName = createRequest.LastName,
                MiddleName = createRequest.MiddleName,
                DateOfBirth = createRequest.DateOfBirth,
                Address = createRequest.Address,
                Postcode = createRequest.Postcode,
                Country = createRequest.Country,
                Email = createRequest.Email,
                PhoneNumber = createRequest.PhoneNumber,
                Type = await userService.IsEmpty() ? UserType.Admin : UserType.Voter  // first user to sign up is admin
            };
            
            return await userService.CreateUser(user, cancellationToken) == null
                ? Results.BadRequest()
                : TypedResults.Created($"/user/{user.UserId}");
        });

        app.MapGet("/user/{userId}", [Authorize] async (string userId, IUserService userService, CancellationToken cancellationToken) =>
        {
            User? user = await userService.GetUser(userId, cancellationToken);

            return user == null ? Results.NotFound() : TypedResults.Ok(user);
        });

        return app;
    }  
}