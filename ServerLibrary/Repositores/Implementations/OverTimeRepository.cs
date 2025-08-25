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

public class OverTimeRepository : IGenericRepositoryInterface<OverTime>
{
    private readonly AppDbContext _context;
    public OverTimeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<OverTime>> GetAll() => await _context.OverTimes.AsNoTracking().Include(_ => _.OverTimeType).ToListAsync();

    public async Task<ResultResponse<OverTime>> GetById(int id)
    {
        var overTime = await FindById(id);
        return overTime is null ? ResultResponse<OverTime>.Failure(ConstantsResponse.NotFound) : ResultResponse<OverTime>.Success(overTime);
    }
    public async Task<GeneralResponse> Create(OverTime model)
    {
        var errorMessage = Validation.ValidateModel<OverTime>(model);
        if (errorMessage.Any()) return InputDataNotValid();
        var overTime = await FindById(model.Id);
        if (overTime is not null) return Existed();
        _context.OverTimes.Add(model);
        await Commit();
        return Success();

    }
    public async Task<GeneralResponse> Update(OverTime model)
    {
        var errorMessage = Validation.ValidateModel<OverTime>(model);
        if (errorMessage.Any() || model.Id < 0) return InputDataNotValid();
        var overTime = await FindById(model.Id);
        if (overTime is null) return NotFound();
        overTime.EndDate = model.EndDate;
        overTime.StartDate = model.StartDate;
        overTime.OverTimeTypeId = model.OverTimeTypeId;
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var overTime = await FindById(id);
        if (overTime is null) return NotFound();
        _context.OverTimes.Remove(overTime);
        await Commit();
        return Success();
    }


    // Re-Useable Method

    // Response Method
    private GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private GeneralResponse NotFound() => new GeneralResponse(false, ConstantsResponse.NotFound);
    private GeneralResponse Existed() => new GeneralResponse(false, ConstantsResponse.Exit);
    private GeneralResponse InputDataNotValid() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);

    //Others
    private async Task<OverTime?> FindById(int id) => await _context.OverTimes.AsNoTracking().Include(_ => _.OverTimeType).FirstOrDefaultAsync(_ => _.Id == id);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<bool> ChechName(string name) => await _context.OverTimeTypes.FirstOrDefaultAsync(_ => _.Name!.Trim().ToLower() == name.Trim().ToLower()) is null ? false : true;
}
