using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace ServerLibrary.Repositores.Implementations;

public class UserRoleRepository
{
    private readonly AppDbContext _context;
    public UserRoleRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<UserRole>> GetAll() => await _context.UserRoles.ToListAsync();
    public async Task<GeneralResponse>Create(UserRole userRole)
    {
        var response = _context.UserRoles.Add(userRole);
        if (response is null) return Unsuccess();
        await Commit();
        return Success();
    }
    public async Task<UserRole?> FindByUserId(int id)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(_ => _.UserId == id);
    }

    public async Task<GeneralResponse> Update(UserRole userRole)
    {
        var exitedUserRole = await FindById(userRole.Id);
        if (exitedUserRole is null) NotFound();
        exitedUserRole!.RoleId = userRole.RoleId;
        await Commit();
        return Success();

    }
    public async Task<GeneralResponse>DeleteByUserId(int id)
    {
        var userRole = await _context.UserRoles.FirstOrDefaultAsync(_ => _.UserId == id);
        if (userRole is null) return NotFound();
        _context.UserRoles.Remove(userRole);
        await Commit();
        return Success();
    }

    private async Task<UserRole?> FindById(int id) =>await _context.UserRoles.FindAsync(id);
    private async Task Commit() => await _context.SaveChangesAsync();
    private static GeneralResponse Unsuccess() => new GeneralResponse(false, nameof(UserRole) + ConstantsResponse.Unsuccess);
    private static GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private static GeneralResponse NotFound() => new GeneralResponse(false, ConstantsResponse.NotFound);
}
