using System.Security.Cryptography;

namespace ServerLibrary.Authentication;

public class RefreshTokenService
{
    public static string GetToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
