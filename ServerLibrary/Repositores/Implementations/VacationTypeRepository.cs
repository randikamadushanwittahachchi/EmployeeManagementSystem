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

public class VacationTypeRepository : IGenericRepositoryInterface<VacationType>
{
    private readonly AppDbContext _context;
    public VacationTypeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<VacationType>> GetAll() => await _context.VacationTypes.ToListAsync();

    public async Task<ResultResponse<VacationType>> GetById(int id)
    {
        var vacationType = await FindById(id);
        return vacationType is null ? ResultResponse<VacationType>.Failure(ConstantsResponse.NotFound) : ResultResponse<VacationType>.Success(vacationType);
    }
    public async Task<GeneralResponse> Create(VacationType model)
    {
        var errorMessage = Validation.ValidateModel<VacationType>(model);
        if (errorMessage.Any()) return InputDataNotValid();
        var vacationType = await FindById(model.Id);
        if (vacationType is not null) return Existed();
        _context.VacationTypes.Add(model);
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse> Update(VacationType model)
    {
        var errorMessage = Validation.ValidateModel<VacationType>(model);
        if (errorMessage.Any() || model.Id < 0) return InputDataNotValid();
        if (await ChechName(model.Name!)) return Existed();
        var vacationType = await FindById(model.Id);
        if (vacationType is null) return NotFound();
        vacationType.Name = model.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var vacationType = await FindById(id);
        if (vacationType is null) return NotFound();
        _context.VacationTypes.Remove(vacationType);
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
    private async Task<VacationType?> FindById(int id) => await _context.VacationTypes.FindAsync(id);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<bool> ChechName(string name) => await _context.VacationTypes.FirstOrDefaultAsync(_ => _.Name!.Trim().ToLower() == name.Trim().ToLower()) is null ? false : true;

}
