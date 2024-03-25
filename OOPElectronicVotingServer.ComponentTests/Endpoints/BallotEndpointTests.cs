using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using OOPElectronicVotingServer.ComponentTests.Extensions;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.BallotContracts;

namespace OOPElectronicVotingServer.ComponentTests.Endpoints;

public class BallotEndpointTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    [Fact]
    public async Task Get_Ballots_Should_Return_All_Ballots()
    {
        // Act
        HttpResponseMessage result = await fixture.VoterClient.GetAsync("ballots?electionId=e67f95e0-4250-4c75-8990-44a77fc35f22");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var ballots = (await result.ReadAsJsonListAsync<Ballot>()).ToList();

        ballots.Should().HaveCount(2);
        ballots.Should().BeEquivalentTo([
            new Ballot
            {
                ElectionId = Guid.Parse("e67f95e0-4250-4c75-8990-44a77fc35f22"),
                UserId = "Seeded1",
                CandidateId = Guid.Parse("6ec3dda1-db7e-43cc-b295-a182683d7fb0"),
                BallotId = Guid.Parse("776b09da-acbd-4452-9a6f-cdb79338d046")
            },
            new Ballot
            {
                ElectionId = Guid.Parse("e67f95e0-4250-4c75-8990-44a77fc35f22"),
                UserId = "Seeded2",
                CandidateId = Guid.Parse("6ec3dda1-db7e-43cc-b295-a182683d7fb0"),
                BallotId = Guid.Parse("3bd743e9-3006-4891-9d95-0d96cad3b15d")
            }
        ]);
    }

    [Fact]
    public async Task Create_Ballot_Should_Return_Created_Ballot()
    {
        // Arrange
        CreateBallotRequest newBallot = new()
        {
            ElectionId = Guid.Parse("483d63bf-1d12-477c-a643-5c28c7d5ae4e"),
            UserId = "TestAccount",
            CandidateId = Guid.Parse("bca7e44e-552b-42c0-b112-45bf52b43e94")
        };

        // Act
        HttpResponseMessage response = await fixture.VoterClient.PostAsJsonAsync("ballot", newBallot);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Ballot? ballot = await response.ReadAsJsonAsync<Ballot>();

        ballot.Should().NotBeNull();
        ballot?.ElectionId.ToString().Should().HaveLength(36);
    }
    
    [Fact]
    public async Task Create_Ballot_With_Invalid_ElectionId_Should_Return_Bad_Request()
    {
        // Arrange
        CreateBallotRequest newBallot = new()
        {
            ElectionId = Guid.NewGuid(),
            UserId = "TestAccount",
            CandidateId = Guid.NewGuid()
        };

        // Act
        HttpResponseMessage response = await fixture.VoterClient.PostAsJsonAsync("ballot", newBallot);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.Content.ReadAsStringAsync()).Should().Be("\"Specified election does not exist\"");
    }
    
    [Fact]
    public async Task Create_Ballot_With_Invalid_CandidateId_Should_Return_Bad_Request()
    {
        // Arrange
        CreateBallotRequest newBallot = new()
        {
            ElectionId = Guid.Parse("483d63bf-1d12-477c-a643-5c28c7d5ae4e"),
            UserId = "TestAccount",
            CandidateId = Guid.NewGuid()
        };

        // Act
        HttpResponseMessage response = await fixture.VoterClient.PostAsJsonAsync("ballot", newBallot);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.Content.ReadAsStringAsync()).Should().Be("\"Specified candidate does not exist\"");
    }
    
    [Fact]
    public async Task Create_Ballot_With_NotMatching_Id_Should_Return_Unauthorised()
    {
        // Arrange
        CreateBallotRequest newBallot = new()
        {
            ElectionId = Guid.NewGuid(),
            UserId = "invalid-auth-id",
            CandidateId = Guid.NewGuid()
        };

        // Act
        HttpResponseMessage response = await fixture.VoterClient.PostAsJsonAsync("ballot", newBallot);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Create_Ballot_With_QrCode_Should_Return_Created_Ballot()
    {
        // Arrange
        CreateBallotRequest newBallot = new()
        {
            ElectionId = Guid.Parse("483d63bf-1d12-477c-a643-5c28c7d5ae4e"),
            UserId = "voteWithQR|B8SX9Pem6cuYitrFzKunXcB23Sk3xbtTB3nSzx5bjqpTSmQ5Cre42xjSzjvrYaRF",
            CandidateId = Guid.Parse("bca7e44e-552b-42c0-b112-45bf52b43e94")
        };

        // Act
        HttpResponseMessage response = await fixture.UnauthorisedClient.PostAsJsonAsync("ballot", newBallot);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Ballot? ballot = await response.ReadAsJsonAsync<Ballot>();

        ballot.Should().NotBeNull();
        ballot?.ElectionId.ToString().Should().HaveLength(36);
    }
    
    [Fact]
    public async Task Create_Ballot_With_Invalid_QrCode_Should_Return_Unauthorised()
    {
        // Arrange
        CreateBallotRequest newBallot = new()
        {
            ElectionId = Guid.Parse("483d63bf-1d12-477c-a643-5c28c7d5ae4e"),
            UserId = "invalid-qr-code",
            CandidateId = Guid.Parse("bca7e44e-552b-42c0-b112-45bf52b43e94")
        };

        // Act
        HttpResponseMessage response = await fixture.UnauthorisedClient.PostAsJsonAsync("ballot", newBallot);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}