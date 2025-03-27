using BaseLibrary.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerLibrary.Authentication;

public class TokenService
{
    private readonly IOptions<JWTSection> _config;
    public TokenService( IOptions<JWTSection> config)
    {
        _config = config;
    }
    public string GetToken(AppUser appUser, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.Key!));
        var credential = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, appUser.FullName),
            new Claim(ClaimTypes.Email, appUser.Email),
            new Claim(ClaimTypes.Role, role)
        };
        var token = new JwtSecurityToken(
            issuer:_config.Value.Issuer,
            audience:_config.Value.Audience,
            claims: userClaims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials:credential
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}