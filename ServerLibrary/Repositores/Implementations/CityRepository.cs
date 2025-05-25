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

    public async Task<City?> GetById(int id) => await FindByIdAsync(id);

    public async Task<GeneralResponse> Create(City model)
    {
        if (await CheckName(model.Name)) return Exited();
        await _context.Cities.AddAsync(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(City model)
    {
        var item = await FindByIdAsync(model.Id);
        if (item == null) return NotFound();
        if (item.Name != model.Name && await CheckName(model.Name)) return Exited();
        item.Name = model.Name;
        item.CountryId = model.CountryId;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindByIdAsyncWithChild(id);
        if (item == null) return NotFound();
        if (item.Towns is not null && item.Towns.Any()) return HasChild();
        _context.Cities.Remove(item);
        await Commit();
        return Success();
    }

    // reusable methods and propety

    private GeneralResponse Success() => new GeneralResponse(true,ConstantsResponse.Success);
    private GeneralResponse NotFound() => new GeneralResponse(false, nameof(City) + ConstantsResponse.NotFound);
    private GeneralResponse HasChild() => new GeneralResponse(false, nameof(City) + ConstantsResponse.HasChild + "of Branch");
    private GeneralResponse Exited() => new GeneralResponse(false, nameof(City) + ConstantsResponse.Exit);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<City?> FindByIdAsync(int id) => await _context.Cities.FindAsync(id);
    private async Task<City?> FindByIdAsyncWithChild(int id) => await _context.Cities.Include(c => c.Towns).FirstOrDefaultAsync(c => c.Id == id);
    private async Task<bool> CheckName(string name) 
    {
        var item = await _context.Cities.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return item is null ? false : true;
    }
}
