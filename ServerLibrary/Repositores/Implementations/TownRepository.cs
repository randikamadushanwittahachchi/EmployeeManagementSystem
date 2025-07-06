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
    public async Task<List<Town>> GetAll() => await _context.Towns
        .AsNoTracking()
        .Include(t => t.City)
        .ToListAsync();

    public async Task<ResultResponse<Town>> GetById(int id)
    {
        var result = await FindById(id);
        return result is null ? ResultResponse<Town>.Failure(ConstantsResponse.ErrorInputData) : ResultResponse<Town>.Success(result);
    }
    public async Task<GeneralResponse> Create(Town model)
    {
        if (model.Name is null) return InputDataNotValidGeneral();
        if(await CheckName(model.Name)) return Exited();
        _context.Towns.Add(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Town model)
    {
        if (model.Id < 0 || model.Name is null) return InputDataNotValidGeneral();
        var item = await FindById(model.Id);
        if (item is null) return NotFound();
        if (await CheckName(model.Name)) return Exited();
        item.Name = model.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await FindById(id);
        if (item is null) return NotFound();
        _context.Towns.Remove(item);
        await Commit();
        return Success();
    }

    // reusable methods and propety
    private GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private GeneralResponse NotFound() => new GeneralResponse(false, nameof(Town) + ConstantsResponse.NotFound);
    private GeneralResponse Exited() => new GeneralResponse(false, nameof(Town) + ConstantsResponse.Exit);
    private GeneralResponse InputDataNotValidGeneral() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<Town?> FindById(int id) => await _context.Towns.FindAsync(id);
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Towns.FirstOrDefaultAsync(_ => _.Name!.ToLower() == name.ToLower());
        return item is null ? false :true;
    }


}
