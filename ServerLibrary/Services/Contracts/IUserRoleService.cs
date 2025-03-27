using BaseLibrary.Entities;

namespace ServerLibrary.Services.Contracts
{
    public interface IUserRoleService
    {
        Task<UserRole?> FindByUserId(int? id);
    }
}
