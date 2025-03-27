using BaseLibrary.Entities;

namespace ServerLibrary.Services.Contracts
{
    public interface IRefreshTokenInfoService
    {
        Task<RefreshTokenInfo?> FindByToken(string token);
        Task Update(RefreshTokenInfo token);
    }
}
