using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;
using OOPElectronicVotingServer.Services.BallotService;
using OOPElectronicVotingServer.Services.QrCodeService;

namespace OOPElectronicVotingServer.Endpoints;

public static class BallotEndpointExtensions
{
    public static WebApplication MapBallotEndpoints(this WebApplication app)
    {
        app.MapPost("/ballot", async Task<Results<UnauthorizedHttpResult, BadRequest, Created<Ballot>>> (
            CreateBallotRequest createRequest,
            IBallotService ballotService,
            IQrCodeService qrCodeService,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            string? userId = context.User.Identity?.Name;

            if ((string.IsNullOrWhiteSpace(userId) || userId != createRequest.UserId)
                && (!string.IsNullOrWhiteSpace(userId) || qrCodeService.GetQrCode(createRequest.UserId) == null))
            {
                return TypedResults.Unauthorized();
            }

            Ballot? ballot = await ballotService.CreateBallot(createRequest, cancellationToken);
    
            return ballot == null
                ? TypedResults.BadRequest()
                : TypedResults.Created("/ballots", ballot);

        }).WithTags("Ballot");

        app.MapGet("/ballots", (Guid electionId, IBallotService ballotService) =>
            TypedResults.Ok(ballotService.GetBallots(electionId))).WithTags("Ballot");

        return app;
    }
}