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

public class EmployeeRepository : IGenericRepositoryInterface<Employee>
{
    // Injecting Data AppDbContext
    readonly AppDbContext _context;
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Employee>> GetAll()
    {
        var employees = await _context.Employees.
            AsNoTracking().
            Include(e => e.Branch).
            ThenInclude(b => b!.Department).
            ThenInclude(d => d!.GeneralDepartment).
            Include(e => e.Town).
            ThenInclude(t => t!.City).
            ThenInclude(c => c!.Country).
            ToListAsync();
        return employees;
    }

    public async Task<ResultResponse<Employee>> GetById(int id)
    {
        var employee = await FindById(id);
        return employee is null ? ResultResponse<Employee>.Failure(ConstantsResponse.NotFound) : ResultResponse<Employee>.Success(employee);
    }
    public async Task<GeneralResponse> Create(Employee model)
    {
        var errorMessage = Validation.ValidateModel<Employee>(model);
        if (errorMessage.Any()) return InputDataNotValidGeneral(); 
        if (await checkedCivilId(model.CivilId)) return Exited();
        _context.Employees.Add(model);
        await Commited();
        return Success();
    }

    public async Task<GeneralResponse> Update(Employee model)
    {
        var errorMessage = Validation.ValidateModel<Employee>(model);
        if (errorMessage.Any() || model.Id < 0 ) return InputDataNotValidGeneral();
        var employee = await FindById(model.Id);
        if (employee is null) return NotFound();
        if ( employee.CivilId != model.CivilId && await checkedCivilId(model.CivilId)) return Exited();
        employee.Name = model.Name!.Trim();
        employee.CivilId = model.CivilId.Trim();
        employee.Addrese = model.Addrese.Trim();
        employee.TelephoneNumber = model.TelephoneNumber.Trim();
        employee.FileName = model.FileName.Trim();
        employee.JobName = model.JobName.Trim();
        employee.Photo = model.Photo.Trim();
        employee.BranchId = model.BranchId;
        employee.TownId = model.TownId;
        await Commited();
        return Success();
    }
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var employee = await FindById(id);
        if (employee is null) return NotFound();
        _context.Employees.Remove(employee);
        await Commited();
        return Success();
    }





    // Resue Mothed

    private async Task<Employee?> FindById(int id) => await _context.Employees.FindAsync(id);
    private async Task<bool> checkedCivilId(string civilId)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(_ => _.CivilId.Trim().ToLower() == civilId.Trim().ToLower());
        return employee is null ? false : true;
    }
    private static GeneralResponse NotFound() => new GeneralResponse(false, ConstantsResponse.NotFound);
    private static GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private static GeneralResponse InputDataNotValidGeneral() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);
    private static GeneralResponse Exited() => new GeneralResponse(false, ConstantsResponse.Exit);
    private async Task Commited() => await _context.SaveChangesAsync();
}
