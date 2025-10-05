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

public class VacationRepository : IGenericRepositoryInterface<Vacation>
{
    private readonly AppDbContext _context;
    public VacationRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Vacation>> GetAll() => await _context.Vacations.AsNoTracking().Include(_ => _.Employee).Include(_ => _.VacationType).ToListAsync();

    public async Task<ResultResponse<Vacation>> GetById(int id)
    {
        var vacation = await _context.Vacations.AsNoTracking().Include(_ => _.VacationType).Include(_=>_.Employee).FirstOrDefaultAsync(_ => _.Id == id);
        return vacation is null ? ResultResponse<Vacation>.Failure(ConstantsResponse.NotFound) : ResultResponse<Vacation>.Success(vacation);
    }
    public async Task<GeneralResponse> Create(Vacation model)
    {
        var errorMessage = Validation.ValidateModel<Vacation>(model);
        if (errorMessage.Any()) return InputDataNotValid();
        var vacation = await FindById(model.Id);
        if (vacation is not null) return Existed();
        _context.Vacations.Add(model);
        await Commit();
        return Success();

    }
    public async Task<GeneralResponse> Update(Vacation model)
    {
        var errorMessage = Validation.ValidateModel<Vacation>(model);
        if (errorMessage.Any() || model.Id < 0) return InputDataNotValid();
        var vacation = await FindById(model.Id);
        if (vacation is  null) return NotFound();
        vacation.StartDate = model.StartDate;
        vacation.EndDate = model.EndDate;
        vacation.VacationTypeId = model.VacationTypeId;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var vacation = await FindById(id);
        if (vacation is null) return NotFound();
        _context.Vacations.Remove(vacation);
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
    private async Task<Vacation?> FindById(int id) => await _context.Vacations.FindAsync(id);
    private async Task Commit() => await _context.SaveChangesAsync();

}
