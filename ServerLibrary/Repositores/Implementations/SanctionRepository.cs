using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
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

public class SanctionRepository : IGenericRepositoryInterface<Sanction>
{
    private readonly AppDbContext _context;
    public SanctionRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Sanction>> GetAll() => await _context.Sanctions.AsNoTracking().Include(_ => _.SanctionType).ToListAsync();

    public async Task<ResultResponse<Sanction>> GetById(int id)
    {
        var sanction = await _context.Sanctions.AsNoTracking().Include(_ => _.SanctionType).Include(_=>_.Employee).FirstOrDefaultAsync(_ => _.Id == id);
        return sanction is null ? ResultResponse<Sanction>.Failure(ConstantsResponse.NotFound) : ResultResponse<Sanction>.Success(sanction);
    }
    public async Task<GeneralResponse> Create(Sanction model)
    {
        var errorMessage = Validation.ValidateModel<Sanction>(model);
        if (errorMessage.Any()) return InputDataNotValid();
        var sanction = await FindById(model.Id);
        if (sanction is not null) return Exist();
        _context.Sanctions.Add(model);
        await Commit();
        return Success();

    }
    public async Task<GeneralResponse> Update(Sanction model)
    {
        var errorMessage = Validation.ValidateModel<Sanction>(model);
        if (errorMessage.Any() || model.Id < 0) return InputDataNotValid();
        var sanction = await FindById(model.Id);
        if (sanction is null) return NotFound();
        sanction.SanctionDate = model.SanctionDate;
        sanction.Date = model.Date;
        sanction.SanctionName = model.SanctionName;
        sanction.SanctionTypeId = model.SanctionTypeId;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var sanction = await FindById(id);
        if (sanction is null) return NotFound();
        _context.Sanctions.Remove(sanction);
        await Commit();
        return Success();
    }



    // Re-Useable Method

    // Response Method
    private GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private GeneralResponse NotFound() => new GeneralResponse(false, ConstantsResponse.NotFound);
    private GeneralResponse Exist() => new GeneralResponse(false, ConstantsResponse.Exit);
    private GeneralResponse InputDataNotValid() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);

    //Others
    private async Task<Sanction?> FindById(int id) => await _context.Sanctions.FindAsync(id);
    private async Task Commit() => await _context.SaveChangesAsync();
}
