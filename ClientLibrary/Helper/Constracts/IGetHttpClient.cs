namespace ClientLibrary.Helper.Constracts;

public interface IGetHttpClient
{
    Task<HttpClient?> GetPrivateHttpClientAsync();
    HttpClient GetPublicHttpClient();
}
