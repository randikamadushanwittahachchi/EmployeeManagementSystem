using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;
using System.Threading.Tasks;

namespace ServerLibrary.Repositores.Implementations;

public class DoctorRepository : IGenericRepositoryInterface<Doctor>
{
    // Injected AppDbContext
    private readonly AppDbContext _context;
    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    // CRUD Operation
    public async Task<List<Doctor>> GetAll() => await _context.Doctors.Include(d=>d.Employee).Include(d=>d.DoctorType).ToListAsync();

    public async Task<ResultResponse<Doctor>> GetById(int id)
    {
        var doctor = await FindById(id);
        return doctor is null ? ResultResponse<Doctor>.Failure(ConstantsResponse.NotFound) : ResultResponse<Doctor>.Success(doctor);
    }
    public async Task<GeneralResponse> Create(Doctor model)
    {
        var validationError = Validation.ValidateModel<Doctor>(model);
        if (validationError.Any()) return InputDataNotValid();
        var doctor = await FindById(model.Id);
        if (doctor is not null) return Exist();
        _context.Doctors.Add(model);
        await Commit();
        return Success();

    }
    public async Task<GeneralResponse> Update(Doctor model)
    {
        var errorMessage = Validation.ValidateModel<Doctor>(model);
        if (errorMessage.Any() || model.Id < 0) InputDataNotValid();
        var doctor = await FindById(model.Id);
        if (doctor is null) return NotFound();
        doctor.DoctorTypeId = model.DoctorTypeId;
        doctor.Date = model.Date;
        doctor.MedicalDiagnose = model.MedicalDiagnose;
        doctor.MedicalRecommndation = model.MedicalRecommndation;
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var doctor = await FindById(id);
        if (doctor is null) return NotFound();
        _context.Doctors.Remove(doctor);
        await Commit();
        return Success();
    }




    // re-usable method

        // Response
    private GeneralResponse InputDataNotValid() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);
    private GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private GeneralResponse NotFound() => new GeneralResponse(false, ConstantsResponse.NotFound);
    private GeneralResponse Exist() => new GeneralResponse(false, ConstantsResponse.Exit);

    // Others
    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<Doctor?> FindById(int id) => await _context.Doctors.FindAsync(id);
}
