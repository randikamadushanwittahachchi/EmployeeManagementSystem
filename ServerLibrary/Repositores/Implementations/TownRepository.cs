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

public class TownRepository : IGenericRepositoryInterface<Town>
{
    // Inject DbContext or any other dependencies here
    private readonly AppDbContext _context;
    public TownRepository(AppDbContext context)
    {
        _context = context;
    }

    // CRUD Operations
    public async Task<List<Town>> GetAll() => await _context.Towns.ToListAsync();

    public async Task<Town?> GetById(int id) => await FindByIdAsync(id);
    public async Task<GeneralResponse> Create(Town model)
    {
        if(!await CheckName(model.Name)) return new GeneralResponse(false, nameof(Town) + ConstantsResponse.Exit);
        _context.Towns.Add(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Town model)
    {
        var item = await FindByIdAsync(model.Id);
        if (item is null) return NotFound();
        item.Name = model.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindByIdAsync(id);
        if (item is null) return NotFound();
        _context.Towns.Remove(item);
        await Commit();
        return Success();
    }

    // reusable methods and propety
    private GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private GeneralResponse NotFound() => new GeneralResponse(false, nameof(Town) + ConstantsResponse.NotFound);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<Town?> FindByIdAsync(int id) => await _context.Towns.FindAsync(id);
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Towns.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return item is null ? false : true;
    }


}
