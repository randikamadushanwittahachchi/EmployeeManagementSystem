using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;

namespace ServerLibrary.Repositores.Implementations
{
    public class DepartmentRepository : IGenericRepositoryInterface<Department>
    {

        // Injecting the AppDbContext
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext? context)
        {
            _context = context ?? throw new ArgumentNullException("Data base is connection error");
        }


        // CRUD Operations

        public async Task<List<Department>> GetAll() => await _context.Departments
            .AsNoTracking()
            .Include(d=>d.GeneralDepartment)
            .ToListAsync();

        public async Task<ResultResponse<Department>> GetById(int id) 
        {
            var result = await FindById(id);
            return result is null ? ResultResponse<Department>.Failure(ConstantsResponse.ErrorInputData): ResultResponse<Department>.Success(result);
        }

        public async Task<GeneralResponse> Create(Department model)
        {
            if (model.Name is null) return InputDataNotValidGeneral();
            if (await CheckName(model.Name!)) return Exited();
            _context.Departments.Add(model);
            await Commite();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department model)
        {
            if (model.Id < 0 || model.Name is null) return InputDataNotValidGeneral();
            var item = await FindById(model.Id);
            if (item is null ) return NotFound();
            if (!string.Equals(model.Name, item.Name, StringComparison.OrdinalIgnoreCase) && await CheckName(model.Name)) return Exited();
            item.Name = model.Name;
            item.GeneralDepartmentId = model.GeneralDepartmentId;
            await Commite();
            return Success();
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await FindByIdWithChild(id);
            if (item is null) return NotFound();
            if (item.Branches is not null && item.Branches.Any()) return HasChild();
            _context.Departments.Remove(item);
            await Commite();
            return Success();
        }


        // reuseble propety and methode

        private  async Task<Department?> FindById(int id) => await _context.Departments.FindAsync(id);
        private async Task<Department?> FindByIdWithChild(int id) => await _context.Departments.Include(d => d.Branches).FirstOrDefaultAsync(d => d.Id == id);
        private static GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
        private static GeneralResponse NotFound() => new GeneralResponse(false, nameof(Department) + ConstantsResponse.NotFound);
        private static GeneralResponse Exited() => new GeneralResponse(false, nameof(Department) + ConstantsResponse.Exit);
        private static GeneralResponse InputDataNotValidGeneral() => new GeneralResponse(false, ConstantsResponse.ErrorInputData);
        private static GeneralResponse HasChild() => new GeneralResponse(false, nameof(Department) + ConstantsResponse.HasChild + "of Branch");
        private async Task Commite() => await _context.SaveChangesAsync();
        private async Task<bool> CheckName(string name)
        {
            var item = await _context.Departments.FirstOrDefaultAsync(_ => _.Name!.Trim().ToLower() == name.Trim().ToLower());
            return item is null ? false : true;
        }

    }
}
