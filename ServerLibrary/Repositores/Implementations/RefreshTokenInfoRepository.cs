using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositores.Implementations
{
    public class RefreshTokenInfoRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenInfoRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException($"Database Null Exception");
        }
        public async Task<RefreshTokenInfo?> Add(RefreshTokenInfo token)
        {
            if (token is null) throw new ArgumentNullException($"{nameof(token)} can't be empty");
            var result = _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<RefreshTokenInfo?> FindByToken(string token)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("Refresh token cannot be empty");

            return await _context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token!.Equals(token));
        }

        public async Task Update(RefreshTokenInfo token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
