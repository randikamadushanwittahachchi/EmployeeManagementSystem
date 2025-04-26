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

    public async Task<List<City>> GetAll() => await _context.Cities.ToListAsync();

    public async Task<City?> GetById(int id) => await FindByIdAsync(id);

    public async Task<GeneralResponse> Create(City model)
    {
        if (!await CheckName(model.Name)) return new GeneralResponse(false, nameof(City) + ConstantsResponse.Exit);
        await _context.Cities.AddAsync(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(City model)
    {
        var item = await FindByIdAsync(model.Id);
        if (item == null) return NotFound();
        item.Name = model.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindByIdAsync(id);
        if (item == null) return NotFound();
        _context.Cities.Remove(item);
        await Commit();
        return Success();
    }

    // reusable methods and propety

    private GeneralResponse Success() => new GeneralResponse(true,ConstantsResponse.Success);
    private GeneralResponse NotFound() => new GeneralResponse(false, nameof(City) + ConstantsResponse.NotFound);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<City?> FindByIdAsync(int id) => await _context.Cities.FindAsync(id);
    private async Task<bool> CheckName(string name) 
    {
        var item = await _context.Cities.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return item == null ? false : true;
    }
}
