using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Services.Abstractions;

namespace OOPElectronicVotingServer.Endpoints;

public static class VoterEndpointExtensions
{
    public static WebApplication MapVoterEndpoints(this WebApplication app)
    {
        app.MapPost("/voter", async (Voter voter, IVoterService voterService, CancellationToken cancellationToken) =>
            await voterService.CreateVoter(voter, cancellationToken) == null
                ? Results.BadRequest()
                : TypedResults.Created($"/voters/{voter.VoterId}"));

        app.MapGet("/voter/{voterId}", async (string voterId, IVoterService voterService, CancellationToken cancellationToken) =>
        {
            Voter? voter = await voterService.GetVoter(voterId, cancellationToken);

            return voter == null ? Results.NotFound() : TypedResults.Ok(voter);
        });

        return app;
    }  
}