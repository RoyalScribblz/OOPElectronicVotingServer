using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;
using OOPElectronicVotingServer.Services.BallotService;
using OOPElectronicVotingServer.Services.CandidateService;
using OOPElectronicVotingServer.Services.ElectionService;
using OOPElectronicVotingServer.Services.QrCodeService;

namespace OOPElectronicVotingServer.Endpoints;

public static class BallotEndpointExtensions
{
    public static WebApplication MapBallotEndpoints(this WebApplication app)
    {
        app.MapPost("/ballot", async Task<Results<UnauthorizedHttpResult, BadRequest<string>, BadRequest, Created<Ballot>>> (
            CreateBallotRequest createRequest,
            IBallotService ballotService,
            IQrCodeService qrCodeService,
            IElectionService electionService,
            ICandidateService candidateService,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            string? userId = context.User.Identity?.Name;

            if ((string.IsNullOrWhiteSpace(userId) || userId != createRequest.UserId)
                && (!string.IsNullOrWhiteSpace(userId) || await qrCodeService.GetQrCode(createRequest.UserId) == null))
            {
                return TypedResults.Unauthorized();
            }

            if (await electionService.GetElection(createRequest.ElectionId) == null)
            {
                return TypedResults.BadRequest("Specified election does not exist");
            }
            
            if (await candidateService.GetCandidate(createRequest.CandidateId) == null)
            {
                return TypedResults.BadRequest("Specified candidate does not exist");
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