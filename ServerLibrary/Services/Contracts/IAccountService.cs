namespace ServerLibrary.Services.Contracts;

public interface IAccountService
{
    Task<T?> AddToDatabase<T>(T? model) where T : class;
}
