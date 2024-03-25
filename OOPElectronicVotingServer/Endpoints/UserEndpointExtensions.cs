using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.UserContracts;
using OOPElectronicVotingServer.Extensions;
using OOPElectronicVotingServer.Services.UserService;

namespace OOPElectronicVotingServer.Endpoints;

public static class UserEndpointExtensions
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/user", async Task<Results<UnauthorizedHttpResult, BadRequest<string>, BadRequest, Created<User>>> (
            CreateUserRequest createRequest,
            IUserService userService,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            string? userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? email = context.User.FindFirst(ClaimTypes.Email)?.Value;

            if (userId == null || email == null)
            {
                return TypedResults.Unauthorized();
            }

            if (await userService.GetUser(userId, cancellationToken) != null)
            {
                return TypedResults.BadRequest("User already exists");
            }
            
            User user = new()
            {
                UserId = userId,
                NationalId = createRequest.NationalId,
                FirstName = createRequest.FirstName,
                LastName = createRequest.LastName,
                MiddleName = createRequest.MiddleName,
                DateOfBirth = createRequest.DateOfBirth,
                Address = createRequest.Address,
                Postcode = createRequest.Postcode,
                Country = createRequest.Country,
                Email = email,
                PhoneNumber = createRequest.PhoneNumber
            };
            
            return await userService.CreateUser(user, cancellationToken) == null
                ? TypedResults.BadRequest()
                : TypedResults.Created($"/user/{user.UserId}", user);
        }).RequireAuthorization().WithTags("User");

        app.MapGet("/user/{userId}", async Task<Results<UnauthorizedHttpResult, NotFound, Ok<User>>> (
            string userId,
            IUserService userService,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            if (context.User.IsAdmin() == false && userId != context.User.Identity?.Name)
            {
                return TypedResults.Unauthorized();
            }
            
            User? user = await userService.GetUser(userId, cancellationToken);

            return user == null ? TypedResults.NotFound() : TypedResults.Ok(user);
        }).RequireAuthorization().WithTags("User");

        return app;
    }  
}