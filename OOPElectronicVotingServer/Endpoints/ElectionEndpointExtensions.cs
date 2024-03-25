using Microsoft.AspNetCore.Http.HttpResults;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;
using OOPElectronicVotingServer.Extensions;
using OOPElectronicVotingServer.Services.ElectionService;

namespace OOPElectronicVotingServer.Endpoints;

public static class ElectionEndpointExtensions
{
    public static WebApplication MapElectionEndpoints(this WebApplication app)
    {
        app.MapPost("/election", async Task<Results<UnauthorizedHttpResult, BadRequest, Created<Election>>> (
            CreateElectionRequest createRequest,
            IElectionService electionService,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            if (context.User.IsAdmin() == false)
            {
                return TypedResults.Unauthorized();
            }
            
            Election? election = await electionService.CreateElection(createRequest, cancellationToken);

            return election == null
                ? TypedResults.BadRequest()
                : TypedResults.Created($"/election/{election.ElectionId}", election);
        }).RequireAuthorization().WithTags("Election");

        app.MapGet("/elections", (IElectionService electionService) =>
            TypedResults.Ok(electionService.GetElections())).WithTags("Election");

        app.MapGet("/election/{electionId:guid}", async Task<Results<NotFound, Ok<GetElectionResponse>>> (
            IElectionService electionService,
            Guid electionId) =>
        {
            GetElectionResponse? election = await electionService.GetElection(electionId);

            return election == null
                ? TypedResults.NotFound()
                : TypedResults.Ok(election);
        }).WithTags("Election");

        return app;
    }  
}