using BaseLibrary.DTOs;
using BaseLibrary.Entities;

namespace ServerLibrary.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<AppUser?> FindUserByEmail(string email);
        Task<AppUser?> FindUserById(int? id);
    }
}
