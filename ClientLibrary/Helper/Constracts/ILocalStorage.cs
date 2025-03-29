namespace ClientLibrary.Helper.Constracts;

public interface ILocalStorage
{
    Task<string?> GetTokenAsync();
    Task SetTokenAsync(string item);
    Task RemoveTokenAsync();

}
