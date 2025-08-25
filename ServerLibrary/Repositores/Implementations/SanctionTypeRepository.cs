using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;

namespace ServerLibrary.Repositores.Implementations;

public class SanctionTypeRepository: IGenericRepositoryInterface<SanctionType>
{
    private readonly AppDbContext _context;
    public SanctionTypeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<SanctionType>> GetAll() => await _context.SanctionTypes.ToListAsync();

    public async Task<ResultResponse<SanctionType>> GetById(int id)
    {
        var sanctionType = await FindById(id);
        return sanctionType is null ? ResultResponse<SanctionType>.Failure(ConstantsResponse.NotFound) : ResultResponse<SanctionType>.Success(sanctionType);
    }

    public async Task<GeneralResponse> Create(SanctionType model)
    {
        var errorMessage = Validation.ValidateModel<SanctionType>(model);
        if (errorMessage.Any()) return InputDataNotValid();
        if (await ChechName(model.Name!)) return Existed();
        _context.SanctionTypes.Add(model);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(SanctionType model)
    {
        var errorMessage = Validation.ValidateModel<SanctionType>(model);
        if (errorMessage.Any() || model.Id < 0) return InputDataNotValid();
        if (await ChechName(model.Name!)) return Existed();
        var sanctionType = await FindById(model.Id);
        if (sanctionType is null) return NotFound();
        sanctionType.Name = model.Name;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var sanctionType = await FindById(id);
        if (sanctionType is null) return NotFound();
        _context.SanctionTypes.Remove(sanctionType);
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
    private async Task<SanctionType?> FindById(int id) => await _context.SanctionTypes.FindAsync(id);
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<bool> ChechName(string name) => await _context.SanctionTypes.FirstOrDefaultAsync(_ => _.Name!.Trim().ToLower() == name.Trim().ToLower()) is null ? false : true;

}
