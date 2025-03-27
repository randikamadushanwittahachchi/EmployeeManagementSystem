using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Services.Contracts;

namespace ServerLibrary.Services.Implementations
{
    class SystemRoleService : ISystemRoleService
    {
        private readonly AppDbContext _context;
        public SystemRoleService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SystemRole?> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new AggregateException("Name Can't be empty");

            return await _context.SystemRoles.FirstOrDefaultAsync(_ =>string.Equals(_.Name,name,StringComparison.OrdinalIgnoreCase));
        }

        public async Task<SystemRole?> Register(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) throw new AggregateException("Name Can't be empty");
            var newRole = await _context.SystemRoles.AddAsync(new SystemRole { Name = role });
            await _context.SaveChangesAsync();
            return newRole.Entity!;
        }
    }
}
