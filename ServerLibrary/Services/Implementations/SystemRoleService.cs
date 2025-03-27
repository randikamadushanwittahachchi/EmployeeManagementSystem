using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Services.Contracts;

namespace ServerLibrary.Services.Implementations
{
    public class SystemRoleService : ISystemRoleService
    {
        private readonly AppDbContext _context;
        public SystemRoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SystemRole?> FindById(int? id)
        {
            if(id is null) throw new ArgumentNullException("Id Can't be empty");

            return await _context.SystemRoles.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<SystemRole?> FindByName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new AggregateException("Name Can't be empty");

            return await _context.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.ToLower() == name!.ToLower());
        }
    }
}
