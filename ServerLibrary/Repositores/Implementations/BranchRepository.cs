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
    public async Task<List<Branch>> GetAll() => await _context.Branches
        .AsNoTracking()
        .Include(b=> b.Department)
        .ToListAsync();

    public async Task<ResultResponse<Branch>> GetById(int id)
    {
        var result = await FindById(id);
        return result is null ? ResultResponse<Branch>.Failure(ConstantsResponse.ErrorInputData) : ResultResponse<Branch>.Success(result);
    }

    public async Task<GeneralResponse> Create(Branch model)
    {
        if (model.Name is null) return InputDataNotValidGeneral();
        if (await CheckName(model.Name!)) return Exited();
        _context.Add(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Branch model)
    {
        if (model.Id < 0 || model.Name is null) return InputDataNotValidGeneral();
        var item = await FindById(model.Id);
        if(item is null) return NotFound();
        if (!string.Equals(model.Name,item.Name,StringComparison.OrdinalIgnoreCase) && await CheckName(model.Name)) return Exited();
        item.Name = model.Name;
        item.DepartmentId = model.DepartmentId;
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindById(id);
        if (item == null) return NotFound();
        _context.Remove(item);
        await Commit();
        return Success();
    }

    // reusable propety and methode
    private static GeneralResponse NotFound() => new GeneralResponse(false, nameof(City) + ConstantsResponse.NotFound);
    private static GeneralResponse Exited() => new GeneralResponse(false, nameof(Branch) + ConstantsResponse.Exit);
    private static GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private static GeneralResponse InputDataNotValidGeneral() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);

    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<Branch?> FindById(int id) => await _context.Branches.AsNoTracking().Include(b => b.Department).FirstOrDefaultAsync(_ => _.Id == id);
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Branches.FirstOrDefaultAsync(_ => _.Name!.ToLower() == name.ToLower());
        return item is null? false:true;
    }
}
