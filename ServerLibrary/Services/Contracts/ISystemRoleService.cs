using BaseLibrary.Entities;

namespace ServerLibrary.Services.Contracts;

public interface ISystemRoleService
{
    Task<SystemRole?> FindByName(string name);
    Task<SystemRole?> Register(string role);
}
