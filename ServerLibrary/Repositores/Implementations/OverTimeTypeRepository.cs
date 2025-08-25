using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
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

public class OverTimeTypeRepository : IGenericRepositoryInterface<OverTimeType>
{
    private readonly AppDbContext _context;
    public OverTimeTypeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<OverTimeType>> GetAll() => await _context.OverTimeTypes.ToListAsync();

    public async Task<ResultResponse<OverTimeType>> GetById(int id)
    {
        var overTimeType = await FindById(id);
        return overTimeType is null ? ResultResponse<OverTimeType>.Failure(ConstantsResponse.NotFound) : ResultResponse<OverTimeType>.Success(overTimeType);

    }
    public async Task<GeneralResponse> Create(OverTimeType model)
    {
        var errorMessage = Validation.ValidateModel<OverTimeType>(model);
        if (errorMessage.Any()) return InputDataNotValid();
        if (await ChechName(model.Name!)) return Existed();
        _context.OverTimeTypes.Add(model);
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> Update(OverTimeType model)
    {
        var errorMessage = Validation.ValidateModel<OverTimeType>(model);
        if (errorMessage.Any() || model.Id < 0) return InputDataNotValid();
        if (await ChechName(model.Name!)) return Existed();
        var overTimeType = await FindById(model.Id);
        if (overTimeType is null) return NotFound();
        overTimeType!.Name = model.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var overTimeType = await FindById(id);
        if (overTimeType is null) return NotFound();
        _context.OverTimeTypes.Remove(overTimeType);
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
    private async Task<OverTimeType?> FindById(int id) => await _context.OverTimeTypes.FindAsync(id);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<bool> ChechName(string name) => await _context.OverTimeTypes.FirstOrDefaultAsync(_ => _.Name!.Trim().ToLower() == name.Trim().ToLower()) is null ? false : true;
}
