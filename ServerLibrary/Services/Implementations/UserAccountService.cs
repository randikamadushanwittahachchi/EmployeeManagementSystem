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

        return await _context.AppUsers.FirstOrDefaultAsync(_ => string.Equals(_.Email, email, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<AppUser?> Register(AppUser appUser)
    {
        var newUser = await _context.AppUsers.AddAsync(appUser);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }
}
