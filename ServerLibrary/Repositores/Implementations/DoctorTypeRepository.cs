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

public class DoctorTypeRepository : IGenericRepositoryInterface<DoctorType>
{
    // Inject App DbCOntext
    private readonly AppDbContext _context;
    public DoctorTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    // CRUD OPeration
    public Task<List<DoctorType>> GetAll() => _context.DoctorTypes.ToListAsync();

    public async Task<ResultResponse<DoctorType>> GetById(int id)
    {
        var doctorType = await FindById(id);
        return doctorType is null ? ResultResponse<DoctorType>.Failure(ConstantsResponse.NotFound) : ResultResponse<DoctorType>.Success(doctorType);
    }

    public async Task<GeneralResponse> Create(DoctorType model)
    {
        var errorMessage = Validation.ValidateModel<DoctorType>(model);
        if (errorMessage.Any()) return InputDataNotValid();
        if (await ChechName(model.Name)) return Existed();
        _context.DoctorTypes.Add(model);
        await Commit();
        return Succuss();
    }
    public async Task<GeneralResponse> Update(DoctorType model)
    {
        var errorMessage = Validation.ValidateModel<DoctorType>(model);
        if (errorMessage.Any() || model.Id < 0) InputDataNotValid();
        if (await ChechName(model.Name)) return Existed();
        var doctorType = await FindById(model.Id);
        if (doctorType is null) return NotFound();
        doctorType.Name = model.Name;
        await Commit();
        return Succuss();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var doctorType = await FindById(id);
        if (doctorType is null) return NotFound();
        _context.DoctorTypes.Remove(doctorType);
        await Commit();
        return Succuss();
    }

    // Reuseable method
    private async Task<DoctorType?> FindById(int id) => await _context.DoctorTypes.FindAsync(id);
    private async Task<bool> ChechName(string name)
    {
        var doctorType = await _context.DoctorTypes.FirstOrDefaultAsync(_ => _.Name.Trim().ToLower() == name.Trim().ToLower());
        return doctorType is null ? false : true;
    }
    private GeneralResponse InputDataNotValid() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);
    private GeneralResponse Existed() => new GeneralResponse(false, ConstantsResponse.Exit);
    private async Task Commit() => await _context.SaveChangesAsync();
    private GeneralResponse NotFound() => new GeneralResponse(false, ConstantsResponse.NotFound);
    private GeneralResponse Succuss() => new GeneralResponse(true, ConstantsResponse.Success);

}
