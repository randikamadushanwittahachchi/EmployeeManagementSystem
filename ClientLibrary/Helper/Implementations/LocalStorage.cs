using Blazored.LocalStorage;
using ClientLibrary.Helper.Constracts;

namespace ClientLibrary.Helper.Implementations
{
    public class LocalStorage(ILocalStorageService localStorageService) : ILocalStorage
    {
        private readonly ILocalStorageService _localStorageService = localStorageService;
        private const string StorageKey = "authentication-key";
        public async Task<string?> GetTokenAsync() => await _localStorageService.GetItemAsStringAsync(StorageKey);
        public async Task SetTokenAsync(string item) => await _localStorageService.SetItemAsStringAsync(StorageKey, item);

        public async Task RemoveTokenAsync() => await _localStorageService.RemoveItemAsync(StorageKey);
    }
}
