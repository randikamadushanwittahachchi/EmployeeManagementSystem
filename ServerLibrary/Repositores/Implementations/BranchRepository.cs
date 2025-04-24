using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositores.Contracts;

namespace ServerLibrary.Repositores.Implementations;

public class BranchRepository : IGenericRepositoryInterface<Branch>
{
    private readonly AppDbContext _context;
    public BranchRepository(AppDbContext? context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context), "Database connection error");
    }


    public Task<GeneralResponse> DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Branch>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Branch?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<GeneralResponse> Update(Branch model)
    {
        throw new NotImplementedException();
    }


    // reusable propety and methode

    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Branches.FirstOrDefaultAsync(_ => string.Equals(_.Name, name, StringComparison.OrdinalIgnoreCase));
        return item is null? false:true;
    }
}
