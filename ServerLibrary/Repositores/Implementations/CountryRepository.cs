using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositores.Implementations;

public class CountryRepository : IGenericRepositoryInterface<Country>
{
    // Inject DbContext or any other dependencies here
    private readonly AppDbContext _context;
    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    // CRUD Operations
    public async Task<List<Country>> GetAll() => await _context.Countries.ToListAsync();
    public async Task<ResultResponse<Country>> GetById(int id)
    {
        var result = await FindById(id);
        return result is null ? ResultResponse<Country>.Failure(ConstantsResponse.ErrorInputData) : ResultResponse<Country>.Success(result);
    }
    public async Task<GeneralResponse> Create(Country model)
    {
        if (model.Name is null) return InputDataNotValidGeneral();
        if (await CheckName(model.Name)) return Exited();
        _context.Countries.Add(model);
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> Update(Country model)
    {
        if (model.Id < 0 || model.Name is null) return InputDataNotValidGeneral();
        var item = await FindById(model.Id);
        if (item is null) return NotFound();
        if (!string.Equals(model.Name,item.Name,StringComparison.OrdinalIgnoreCase) && await CheckName(model.Name)) return Exited();
        item.Name = model.Name;
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindByIdWithChild(id);
        if (item == null) return NotFound();
        if (item.Cities is not null && item.Cities.Any()) return HasChild();
        _context.Countries.Remove(item);
        await Commit();
        return Success();
    }

    // reusable methods and propety

    private static GeneralResponse InputDataNotValidGeneral() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);
    private static GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private static GeneralResponse NotFound() => new GeneralResponse(false, nameof(Country) + ConstantsResponse.NotFound);
    private static GeneralResponse Exited() => new GeneralResponse(false, nameof(Country) + ConstantsResponse.Exit);
    private static GeneralResponse HasChild() => new GeneralResponse(false, nameof(Country) + ConstantsResponse.HasChild + "of City");
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<Country?> FindById(int id) => await _context.Countries.FindAsync(id);
    private async Task<Country?> FindByIdWithChild(int id) => await _context.Countries.Include(c => c.Cities).FirstOrDefaultAsync(c => c.Id == id);
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Countries.FirstOrDefaultAsync(_ => _.Name!.Trim().ToLower() == name.Trim().ToLower());
        return item is null ? false : true;
    }
}
