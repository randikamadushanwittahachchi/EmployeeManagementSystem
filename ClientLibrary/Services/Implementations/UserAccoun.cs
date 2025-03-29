using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations;
public class UserAccounm(IGetHttpClient getHttpClient) : IUserAccountService
{
    private readonly IGetHttpClient _getHttpClient = getHttpClient;
    public async Task<GeneralResponse> CreateAsync(Register user)
    {

    }

    public Task<GeneralResponse> RefreshTokenAsync(RefreshToken token)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponse> SignInAsync(Login user)
    {
        throw new NotImplementedException();
    }
}
