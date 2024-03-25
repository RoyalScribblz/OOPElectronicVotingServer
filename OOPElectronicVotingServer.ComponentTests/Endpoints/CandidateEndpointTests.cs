using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using OOPElectronicVotingServer.ComponentTests.Extensions;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.CandidateContracts;

namespace OOPElectronicVotingServer.ComponentTests.Endpoints;

public class CandidateEndpointTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    [Fact]
    public async Task Get_Candidates_Should_Return_All_Candidates()
    {
        // Act
        HttpResponseMessage response = await fixture.VoterClient.GetAsync("candidates");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var candidates = await response.ReadAsJsonListAsync<Candidate>();

        candidates.Should().HaveCount(7);
    }

    [Fact]
    public async Task Create_Candidate_Should_Return_Created_Candidate()
    {
        // Arrange
        CreateCandidateRequest newCandidate = new()
        {
            Name = Guid.NewGuid().ToString(),
            ImageUrl = "https://" + Guid.NewGuid() + ".com/" + Guid.NewGuid() + ".jpeg",
            Colour = $"{new Random().Next(0x1000000):X6}"
        };

        // Act
        HttpResponseMessage response = await fixture.AdminClient.PostAsJsonAsync("candidate", newCandidate);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Candidate? candidate = await response.ReadAsJsonAsync<Candidate>();

        candidate.Should().NotBeNull();

        using (new AssertionScope())
        {
            candidate!.CandidateId.ToString().Should().HaveLength(36);
            candidate.Name.Should().Be(newCandidate.Name);
            candidate.ImageUrl.Should().Be(newCandidate.ImageUrl);
            candidate.Colour.Should().Be(newCandidate.Colour);
        }
    }
    
    [Fact]
    public async Task Create_Candidate_Without_Admin_Claim_Should_Return_Unauthorised()
    {
        // Arrange
        CreateCandidateRequest newCandidate = new()
        {
            Name = Guid.NewGuid().ToString(),
            ImageUrl = "https://" + Guid.NewGuid() + ".com/" + Guid.NewGuid() + ".jpeg",
            Colour = $"{new Random().Next(0x1000000):X6}"
        };

        // Act
        HttpResponseMessage response = await fixture.VoterClient.PostAsJsonAsync("candidate", newCandidate);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Create_Candidate_Without_Authorisation_Should_Return_Unauthorised()
    {
        // Arrange
        CreateCandidateRequest newCandidate = new()
        {
            Name = Guid.NewGuid().ToString(),
            ImageUrl = "https://" + Guid.NewGuid() + ".com/" + Guid.NewGuid() + ".jpeg",
            Colour = $"{new Random().Next(0x1000000):X6}"
        };

        // Act
        HttpResponseMessage response = await fixture.UnauthorisedClient.PostAsJsonAsync("candidate", newCandidate);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}