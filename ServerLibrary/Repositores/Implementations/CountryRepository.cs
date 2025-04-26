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
    public async Task<GeneralResponse> Create(Country model)
    {
        if (!await CheckName(model.Name)) return new GeneralResponse(false, nameof(Country) + ConstantsResponse.Exit);
        _context.Countries.Add(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindByIdAsync(id);
        if(item == null) return new GeneralResponse(false,nameof(Country) + ConstantsResponse.NotFound);
        _context.Countries.Remove(item);
        await Commit();
        return Success();
    }

    public async Task<List<Country>> GetAll() => await _context.Countries.ToListAsync();

    public async Task<Country?> GetById(int id) => await FindByIdAsync(id);

    public async Task<GeneralResponse> Update(Country model)
    {
        var item = await FindByIdAsync(model.Id);
        if (item is null) return new GeneralResponse(false, nameof(Country) + ConstantsResponse.NotFound);
        item.Name = model.Name;
        await Commit();
        return Success();
    }

    // reusable methods and propety
    private GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private GeneralResponse NotFound() => new GeneralResponse(false, nameof(Country) + ConstantsResponse.NotFound);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<Country?> FindByIdAsync(int id) => await _context.Countries.FindAsync(id);
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Countries.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return item is null ? false : true;
    }
}
