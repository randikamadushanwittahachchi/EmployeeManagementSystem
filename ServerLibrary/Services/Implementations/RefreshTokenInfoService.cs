using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Services.Contracts;

namespace ServerLibrary.Services.Implementations;

public class RefreshTokenInfoService : IRefreshTokenInfoService
{
    private readonly AppDbContext _context;
    public RefreshTokenInfoService(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException($"Database Null Exception");
    }

    public async Task<RefreshTokenInfo?> FindByToken(string token)
    {
        if(string.IsNullOrEmpty(token)) throw new ArgumentNullException("Refresh token cannot be empty");

        return await _context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token!.Equals(token));
    }

    public async Task Update(RefreshTokenInfo token)
    {
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync();
    }
}
