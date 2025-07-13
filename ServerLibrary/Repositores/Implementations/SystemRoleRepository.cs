using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositores.Implementations;

public class SystemRoleRepository
{
    private readonly AppDbContext _context;
    public SystemRoleRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<SystemRole>> GetAll() => await _context.SystemRoles.ToListAsync();
    public async Task<SystemRole?> GetById(int id) => await FindById(id);
    public async Task<SystemRole?> GetByName(string name) 
    { 
        var systemRole =  await FindByName(name);
        if (systemRole is null && string.Equals(name.Trim() , Constants.User.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            await Create(new SystemRole { Name = Constants.User });
        }
        else if(systemRole is null && string.Equals(name.Trim(),Constants.Admin.Trim(),StringComparison.OrdinalIgnoreCase))
        {
            await Create(new SystemRole { Name = Constants.User });
        }
        return await FindByName(name);
    }
    public async Task<GeneralResponse> Create(SystemRole systemRole)
    {
        var item = await FindByName(systemRole.Name!);
        if (item is not null) return Unsuccess();
        _context.SystemRoles.Add(systemRole);
        await Commit();
        return Success();
        
    }
    private static GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private async Task Commit() => await _context.SaveChangesAsync();
    private static GeneralResponse Unsuccess() => new GeneralResponse(false, nameof(SystemRole) + ConstantsResponse.Unsuccess);
    private async Task<SystemRole?>FindById(int id) => await _context.SystemRoles.FindAsync(id);
    private async Task<SystemRole?>FindByName(string name) => await _context.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Trim().ToLower() == name!.Trim().ToLower());
}
