using BaseLibrary.DTOs;
using ClientLibrary.Helper.Constracts;

namespace ClientLibrary.Helper.Implementations;

public class GetHttpClient(ILocalStorage localStorage,IHttpClientFactory httpClientFactory, ISerialization serialization) : IGetHttpClient
{
    private readonly ILocalStorage _localStorage = localStorage;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ISerialization _serialization = serialization;
    private const string HeaderKey = "Authorization";
    private const string ClientType = "SystemApiClinet";
    public async Task<HttpClient?> GetPrivateHttpClientAsync()
    {
        HttpClient client = _httpClientFactory.CreateClient(ClientType);
        var stringToken = await _localStorage.GetTokenAsync();
        if (string.IsNullOrEmpty(stringToken)) return client;

        var deserializeToken = _serialization.DeserializeJsonString<UserSession>(stringToken);
        if (deserializeToken == null) return client;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializeToken.Token);
        return client;
    }

    public HttpClient GetPublicHttpClientAsync()
    {
        HttpClient client = _httpClientFactory.CreateClient(ClientType);
        client.DefaultRequestHeaders.Remove(HeaderKey);
        return client;

    }
}
