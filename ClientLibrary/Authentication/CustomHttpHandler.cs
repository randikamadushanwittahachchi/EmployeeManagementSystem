using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Services.Contracts;
using System.Net;

namespace ClientLibrary.Authentication;

public class CustomHttpHandler(ILocalStorage localStorage, ISerialization serialization, IUserAccountService userAccountService) : DelegatingHandler
{
    private readonly ILocalStorage _localStorage = localStorage;
    private readonly ISerialization _serialization = serialization;
    private readonly IUserAccountService _userAccountService = userAccountService;
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
        bool registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
        bool refreshTokenUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");

        //check, is not login/register/refresh
        if(loginUrl || registerUrl || refreshTokenUrl) return await base.SendAsync(request, cancellationToken);

        var result = await base.SendAsync(request,cancellationToken);

        //check,response is unautherize
        if(result.StatusCode == HttpStatusCode.Unauthorized)
        {
            //check, token is empty
            var stringToken = await _localStorage.GetTokenAsync();
            if (stringToken is null) return result;

            var deserializedToken = _serialization.DeserializeJsonString<UserSession>(stringToken);
            if (deserializedToken is null) return result;

            string token = string.Empty;
            try
            {
                token = request.Headers.Authorization!.Parameter!;
            }
            catch { };

            //check , header is ok
            if (string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer",deserializedToken.Token);
                return await base.SendAsync(request, cancellationToken);
            }

            // change the token
            var jwtToken = await GetRefreshToken(deserializedToken.RefrshToken!);
            if (jwtToken is null) return result;

            // send again request
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer",jwtToken);
            return await base.SendAsync(request, cancellationToken);
        }
        return result;

    }

    private async Task<string?> GetRefreshToken(string refreshToken)
    {
         var result = await _userAccountService.RefreshTokenAsync(new RefreshToken { Token = refreshToken});
        string? serializedToken = _serialization.SerializeModelObject(new UserSession { RefrshToken= result!.RefreshToken, Token = result!.Token});
        await _localStorage.SetTokenAsync(serializedToken!);
        return result!.Token;
    }
}
