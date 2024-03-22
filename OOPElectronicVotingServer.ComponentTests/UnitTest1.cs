using FluentAssertions;
using OOPElectronicVotingServer.ComponentTests.Extensions;
using OOPElectronicVotingServer.Endpoints.Contracts.ElectionContracts;

namespace OOPElectronicVotingServer.ComponentTests;

public class UnitTest1(TestFixture fixture) : IClassFixture<TestFixture>
{
    [Fact]
    public async Task Test1()
    {
        var x = await fixture.Client.GetAsync("elections");

        var y = await x.ReadAsJsonListAsync<GetElectionResponse>();

        y.Count().Should().Be(3);
    }
}