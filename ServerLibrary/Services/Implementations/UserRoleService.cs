using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Services.Contracts;

namespace ServerLibrary.Services.Implementations
{
    public class UserRoleService : IUserRoleService
    {
        private readonly AppDbContext _context;
        public UserRoleService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserRole?> FindByUserId(int? id)
        {
            if (id is null) throw new ArgumentNullException($"{nameof(id)} can't be empty");

            return await _context.UserRoles.FirstOrDefaultAsync(_ => _.UserId == id);
        }
    }
}
