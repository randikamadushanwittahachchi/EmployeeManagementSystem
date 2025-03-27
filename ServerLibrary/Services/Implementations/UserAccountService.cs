using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Services.Contracts;

namespace ServerLibrary.Services.Implementations;

public class UserAccountService : IUserAccountService
{
    private readonly AppDbContext _context;
    public UserAccountService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<AppUser?> FindUserByEmail(string email)
    {
        if(string.IsNullOrWhiteSpace(email))throw new ArgumentNullException("Email can't be empty");

        return await _context.AppUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower() == email!.ToLower());
    }
    public async Task<AppUser?> FindUserById(int? id)
    {
        if (id == null) throw new ArgumentNullException("user id cannot be null");

        return await _context.AppUsers.FirstOrDefaultAsync(_ => _.Id!.Equals(id));
    }
}
