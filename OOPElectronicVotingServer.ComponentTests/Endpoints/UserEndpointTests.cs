using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using OOPElectronicVotingServer.ComponentTests.Extensions;
using OOPElectronicVotingServer.Database.Dtos;
using OOPElectronicVotingServer.Endpoints.Contracts.UserContracts;

namespace OOPElectronicVotingServer.ComponentTests.Endpoints;

public class UserEndpointTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    [Fact]
    public async Task Get_User_With_Valid_Id_Should_Get_User()
    {
        // Act
        HttpResponseMessage response = await fixture.VoterClient.GetAsync("user/TestAccount");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        User? user = await response.ReadAsJsonAsync<User>();

        user.Should().NotBeNull();

        using (new AssertionScope())
        {
            user!.UserId.Should().Be("TestAccount");
            user.Email.Should().Be("TestAccount");
            user.NationalId.Should().HaveLength(36);
            user.FirstName.Should().HaveLength(36);
            user.LastName.Should().HaveLength(36);
            user.MiddleName.Should().HaveLength(36);
            user.DateOfBirth.Should().BeBefore(DateTime.Now.AddYears(-18));
            user.Address.Should().HaveLength(36);
            user.Postcode.Should().HaveLength(36);
            user.Country.Should().HaveLength(36);
            user.PhoneNumber.Should().HaveLength(36);
        }
    }
    
    [Fact]
    public async Task Get_User_With_Invalid_Id_Should_Return_NotFound()
    {
        // Act
        HttpResponseMessage response = await fixture.AdminClient.GetAsync("user/AdminTestAccount");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task Get_User_With_NotMatching_Id_Should_Return_Unauthorised()
    {
        // Act
        HttpResponseMessage response = await fixture.VoterClient.GetAsync("user/other-users-id");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Get_User_With_No_Authorisation_Should_Return_Unauthorised()
    {
        // Act
        HttpResponseMessage response = await fixture.UnauthorisedClient.GetAsync("user/account-id");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Create_User_Should_Return_Created_User()
    {
        // Arrange
        CreateUserRequest newUser = new()
        {
            NationalId = Guid.NewGuid().ToString(),
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString(),
            MiddleName = Guid.NewGuid().ToString(),
            DateOfBirth = DateTime.Now.AddYears(-18),
            Address = Guid.NewGuid().ToString(),
            Postcode = Guid.NewGuid().ToString(),
            Country = Guid.NewGuid().ToString(),
            PhoneNumber = Guid.NewGuid().ToString()
        };
        
        // Act
        HttpResponseMessage response = await fixture.AdminClient.PostAsJsonAsync("user", newUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        User? user = await response.ReadAsJsonAsync<User>();

        user.Should().NotBeNull();

        using (new AssertionScope())
        {
            user!.UserId.Should().Be("AdminTestAccount");
            user.Email.Should().Be("AdminTestAccount");
            user.NationalId.Should().Be(newUser.NationalId);
            user.FirstName.Should().Be(newUser.FirstName);
            user.LastName.Should().Be(newUser.LastName);
            user.MiddleName.Should().Be(newUser.MiddleName);
            user.DateOfBirth.Should().Be(newUser.DateOfBirth);
            user.Address.Should().Be(newUser.Address);
            user.Postcode.Should().Be(newUser.Postcode);
            user.Country.Should().Be(newUser.Country);
            user.PhoneNumber.Should().Be(newUser.PhoneNumber);
        }
    }
    
    [Fact]
    public async Task Create_Duplicate_User_Should_Return_Bad_Request()
    {
        // Arrange
        CreateUserRequest newUser = new()
        {
            NationalId = Guid.NewGuid().ToString(),
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString(),
            MiddleName = Guid.NewGuid().ToString(),
            DateOfBirth = DateTime.Now.AddYears(-18),
            Address = Guid.NewGuid().ToString(),
            Postcode = Guid.NewGuid().ToString(),
            Country = Guid.NewGuid().ToString(),
            PhoneNumber = Guid.NewGuid().ToString()
        };
        
        // Act
        HttpResponseMessage response = await fixture.VoterClient.PostAsJsonAsync("user", newUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.Content.ReadAsStringAsync()).Should().Be("\"User already exists\"");
    }
    
    [Fact]
    public async Task Create_User_Without_Authorisation_Should_Return_Unauthorised()
    {
        // Arrange
        CreateUserRequest newUser = new()
        {
            NationalId = Guid.NewGuid().ToString(),
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString(),
            MiddleName = Guid.NewGuid().ToString(),
            DateOfBirth = DateTime.Now.AddYears(-18),
            Address = Guid.NewGuid().ToString(),
            Postcode = Guid.NewGuid().ToString(),
            Country = Guid.NewGuid().ToString(),
            PhoneNumber = Guid.NewGuid().ToString()
        };
        
        // Act
        HttpResponseMessage response = await fixture.UnauthorisedClient.PostAsJsonAsync("user", newUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}