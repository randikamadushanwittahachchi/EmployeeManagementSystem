using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;

namespace ServerLibrary.Repositores.Implementations;

public class BranchRepository : IGenericRepositoryInterface<Branch>
{
    private readonly AppDbContext _context;
    public BranchRepository(AppDbContext? context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context), "Database connection error");
    }


    // crud operation
    public async Task<List<Branch>> GetAll() => await _context.Branches.ToListAsync();

    public async Task<Branch?> GetById(int id) => await FindByIdAsync(id);

    public async Task<GeneralResponse> Create(Branch model)
    {
        if (!await CheckName(model.Name)) return new GeneralResponse(false, nameof(Branch) + ConstantsResponse.Exit);
        _context.Add(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Branch model)
    {
        var item = await FindByIdAsync(model.Id);
        if(item == null) return NotFound();
        item.Name = model.Name;
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindByIdAsync(id);
        if (item == null) return NotFound();
        _context.Remove(item);
        await Commit();
        return Success();
    }

    // reusable propety and methode
    private GeneralResponse NotFound() => new GeneralResponse(false, nameof(City) + ConstantsResponse.NotFound);
    private GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<Branch?> FindByIdAsync(int id) => await _context.Branches.FindAsync(id);
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Branches.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return item is null? false:true;
    }
}
