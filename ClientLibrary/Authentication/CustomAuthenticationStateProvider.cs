using BaseLibrary.DTOs;
using ClientLibrary.Helper.Constracts;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Authentication;

public class CustomAuthenticationStateProvider(ILocalStorage localStorage, ISerialization serialization) : AuthenticationStateProvider
{
    private readonly ILocalStorage _localStorage = localStorage;
    private readonly ISerialization _serialization = serialization;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var stringToken = await  _localStorage.GetTokenAsync();
        if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(_anonymous));

        var deserializeToken = _serialization.DeserializeJsonString<UserSession>(stringToken);
        if(deserializeToken is null) return await Task.FromResult(new AuthenticationState(_anonymous));

        var userClaims = DecryptToken(deserializeToken.Token!);
        if (userClaims is null) return await Task.FromResult(new AuthenticationState(_anonymous));

        var claimsPrincipal = SetClaimsPrincipal(userClaims);
        if (claimsPrincipal is null) return await Task.FromResult(new AuthenticationState(_anonymous));

        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public async Task UpdateAuthenticationStateAsync(UserSession userSession)
    {
        var claimsPrincipal = new ClaimsPrincipal();
        if(userSession.Token != null || userSession.RefrshToken != null)
        {
            var serializToken = _serialization.SerializeModelObject<UserSession>(userSession);
            await _localStorage.SetTokenAsync(serializToken!);
            var userClaims = DecryptToken(userSession.Token!);
            var claimPrincipals = SetClaimsPrincipal(userClaims);
        }
        else
        {
            await _localStorage.RemoveTokenAsync();
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private static ClaimsPrincipal SetClaimsPrincipal(CustomUserClaims? claims)
    {
        if (claims is null) return new ClaimsPrincipal();

        return new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, claims.Id),
                new Claim(ClaimTypes.Name, claims.Name),
                new Claim(ClaimTypes.Email, claims.Email),
                new Claim(ClaimTypes.Role, claims.Role),
                new Claim("IsAutherize", claims.IsAutherize)
            },"JwtAuth"));
    }

    private static CustomUserClaims DecryptToken(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);
        var userId = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        var userName = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name)?.Value ?? "";
        var userEmail = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email)?.Value ?? "";
        var userRole = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role)?.Value ?? "";
        var IsAutherize = token.Claims.FirstOrDefault(_ => _.Type == "IsAutherize")?.Value ?? "false";
        return new CustomUserClaims(userId, userName, userEmail, userRole,IsAutherize);
    }
}
