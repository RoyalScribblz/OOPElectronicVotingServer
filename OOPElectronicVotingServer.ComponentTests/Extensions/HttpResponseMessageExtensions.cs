using System.Net.Http.Json;

namespace OOPElectronicVotingServer.ComponentTests.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<T?> ReadAsJsonAsync<T>(this HttpResponseMessage responseMessage) =>
        await responseMessage.Content.ReadFromJsonAsync<T>();

    public static async Task<IEnumerable<T>> ReadAsJsonListAsync<T>(this HttpResponseMessage responseMessage) =>
        await responseMessage.Content.ReadFromJsonAsync<IEnumerable<T>>() ?? [];
}