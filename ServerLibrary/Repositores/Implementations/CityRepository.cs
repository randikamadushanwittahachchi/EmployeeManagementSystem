using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;

namespace ServerLibrary.Repositores.Implementations;

public class CityRepository : IGenericRepositoryInterface<City>
{
    // Inject DbContext or any other dependencies here

    private readonly AppDbContext _context;
    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    // CRUD Operations

    public async Task<List<City>> GetAll() => await _context.Cities
        .AsNoTracking()
        .Include(c => c.Country)
        .ToListAsync();

    public async Task<ResultResponse<City>> GetById(int id) 
    {
        var result = await FindById(id);
        return result is null ? ResultResponse<City>.Failure(ConstantsResponse.ErrorInputData) : ResultResponse<City>.Success(result);
    }

    public async Task<GeneralResponse> Create(City model)
    {
        if (model.Name is null) return InputDataNotValidGeneral();
        if (await CheckName(model.Name)) return Exited();
        await _context.Cities.AddAsync(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(City model)
    {
        if (model.Id < 0 || model.Name is null) InputDataNotValidGeneral();
        var item = await FindById(model.Id);
        if (item == null) return NotFound();
        if (!string.Equals(model.Name,item.Name,StringComparison.OrdinalIgnoreCase) && await CheckName(model.Name!)) return Exited();
        item.Name = model.Name!;
        item.CountryId = model.CountryId;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindByIdWithChild(id);
        if (item == null) return NotFound();
        if (item.Towns is not null && item.Towns.Any()) return HasChild();
        _context.Cities.Remove(item);
        await Commit();
        return Success();
    }

    // reusable methods and propety

    private static GeneralResponse Success() => new GeneralResponse(true,ConstantsResponse.Success);
    private static GeneralResponse NotFound() => new GeneralResponse(false, nameof(City) + ConstantsResponse.NotFound);
    private static GeneralResponse HasChild() => new GeneralResponse(false, nameof(City) + ConstantsResponse.HasChild + "of Branch");
    private static GeneralResponse Exited() => new GeneralResponse(false, nameof(City) + ConstantsResponse.Exit);
    private static GeneralResponse InputDataNotValidGeneral() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<City?> FindById(int id) => await _context.Cities.AsNoTracking().Include(c => c.Country).FirstOrDefaultAsync(c => c.Id == id);
    private async Task<City?> FindByIdWithChild(int id) => await _context.Cities.AsNoTracking().Include(c => c.Towns).FirstOrDefaultAsync(c => c.Id == id);
    private async Task<bool> CheckName(string name) 
    {
        var item = await _context.Cities.FirstOrDefaultAsync(_ => _.Name!.Trim().ToLower() == name.Trim().ToLower());
        return item is null ? false : true;
    }
}
