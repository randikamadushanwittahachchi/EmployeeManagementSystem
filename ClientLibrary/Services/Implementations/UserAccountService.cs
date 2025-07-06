using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations;
public class UserAccounmService(IGetHttpClient getHttpClient) : IUserAccountService
{
    public const string AuthUrl = "api/authentication";
    private readonly IGetHttpClient _getHttpClient = getHttpClient;

    public async Task<GeneralResponse?> CreateAsync(Register user)
    {
        var httpClient = _getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error Occured");

        return await result.Content.ReadFromJsonAsync<GeneralResponse>();
    }
    public async Task<LoginResponse?> SignInAsync(Login user)
    {
        var httpClient = _getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
        if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error Occured");

        return await result.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<LoginResponse?> RefreshTokenAsync(RefreshToken token)
    {
        var httpClient = _getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", token);
        if (!result.IsSuccessStatusCode) return new LoginResponse(false,"Error Occured");

        return await result.Content.ReadFromJsonAsync<LoginResponse>();
    }
}
