using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using OOPElectronicVotingServer.ComponentTests.Extensions;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

namespace OOPElectronicVotingServer.ComponentTests.Endpoints;

public class ElectionEndpointTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    [Fact]
    public async Task Get_Elections_Should_Return_All_Elections()
    {
        // Act
        HttpResponseMessage result = await fixture.VoterClient.GetAsync("elections");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var elections = await result.ReadAsJsonListAsync<GetElectionResponse>();

        elections.Should().HaveCount(4);
    }

    [Fact]
    public async Task Get_Election_With_Valid_Id_Should_Return_Election()
    {
        // Arrange
        const string electionId = "dc6703e2-589d-42bb-aaa6-30cc09d27822";
        
        // Act
        HttpResponseMessage result = await fixture.VoterClient.GetAsync($"election/{electionId}");
        
        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        GetElectionResponse? election = await result.ReadAsJsonAsync<GetElectionResponse>();

        election.Should().NotBeNull();
        election!.ElectionId.Should().Be(electionId);
    }
    
    [Fact]
    public async Task Get_Election_With_Invalid_Id_Should_Return_NotFound()
    {
        // Arrange
        string electionId = Guid.NewGuid().ToString();
        
        // Act
        HttpResponseMessage result = await fixture.VoterClient.GetAsync($"election/{electionId}");
        
        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_Election_With_Admin_Claim_Should_Return_Created_Election()
    {
        // Arrange
        CreateElectionRequest newElection = new()
        {
            Name = Guid.NewGuid().ToString(),
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = DateTime.Now.AddDays(1),
            CandidateIds = [Guid.Parse("6ec3dda1-db7e-43cc-b295-a182683d7fb0"), 
                Guid.Parse("93da166e-1217-470e-8346-b3e871f672ad"), Guid.Parse("6618f3ef-62f8-4eca-a33d-203c792e67af")]
        };

        // Act
        HttpResponseMessage result = await fixture.AdminClient.PostAsJsonAsync("election", newElection);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);

        Election? election = await result.ReadAsJsonAsync<Election>();

        election.Should().NotBeNull();

        using (new AssertionScope())
        {
            election!.Name.Should().Be(newElection.Name);
            election.StartTime.Should().Be(newElection.StartTime);
            election.EndTime.Should().Be(newElection.EndTime);
            election.CandidateIds.Should().BeEquivalentTo(newElection.CandidateIds);
        }
    }
    
    [Fact]
    public async Task Create_Election_Without_Admin_Claim_Should_Return_Unauthorised()
    {
        // Arrange
        CreateElectionRequest newElection = new()
        {
            Name = Guid.NewGuid().ToString(),
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = DateTime.Now.AddDays(1),
            CandidateIds = [Guid.Parse("6ec3dda1-db7e-43cc-b295-a182683d7fb0"), 
                Guid.Parse("93da166e-1217-470e-8346-b3e871f672ad"), Guid.Parse("6618f3ef-62f8-4eca-a33d-203c792e67af")]
        };

        // Act
        HttpResponseMessage result = await fixture.VoterClient.PostAsJsonAsync("election", newElection);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Create_Election_Without_Authorisation_Should_Return_Unauthorised()
    {
        // Arrange
        CreateElectionRequest newElection = new()
        {
            Name = Guid.NewGuid().ToString(),
            StartTime = DateTime.Now.AddDays(-1),
            EndTime = DateTime.Now.AddDays(1),
            CandidateIds = [Guid.Parse("6ec3dda1-db7e-43cc-b295-a182683d7fb0"), 
                Guid.Parse("93da166e-1217-470e-8346-b3e871f672ad"), Guid.Parse("6618f3ef-62f8-4eca-a33d-203c792e67af")]
        };

        // Act
        HttpResponseMessage result = await fixture.UnauthorisedClient.PostAsJsonAsync("election", newElection);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}