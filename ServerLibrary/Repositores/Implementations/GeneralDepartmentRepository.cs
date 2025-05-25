using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;

namespace ServerLibrary.Repositores.Implementations
{
    public class GeneralDepartmentRepository : IGenericRepositoryInterface<GeneralDepartment>
    {
        // Injecting the AppDbContext

        private readonly AppDbContext _context;

        public GeneralDepartmentRepository(AppDbContext? context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context),"Database connection error");
        }


        // CRUD Operations

        public async Task<List<GeneralDepartment>> GetAll() => await _context.GeneralDepartments.ToListAsync();
        public async Task<GeneralDepartment?> GetById(int id) => await FindById(id);

        public async Task<GeneralResponse> Create(GeneralDepartment model)
        {
            if (await CheckName(model.Name)) return Exited();
            _context.GeneralDepartments.Add(model);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(GeneralDepartment model)
        {
            var item = await FindById(model.Id);
            if (item is null) return NotFound();
            if (item.Name != model.Name && await CheckName(model.Name)) return Exited();
            item.Name = model.Name;
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await FindByIdWithChild(id);
            if (item is null) return NotFound();
            if (item.Departments is not null && item.Departments.Any()) return HasChild();
            _context.GeneralDepartments.Remove(item);
            await Commit();
            return Success();
        }



        // reuseble propety and methode
        private async Task Commit() =>await _context.SaveChangesAsync();
        private async Task<GeneralDepartment?> FindById(int id) => await _context.GeneralDepartments.FindAsync(id);
        private async Task<GeneralDepartment?> FindByIdWithChild(int id) => await _context.GeneralDepartments.Include(gd => gd.Departments).FirstOrDefaultAsync(gd => gd.Id == id);
        private static GeneralResponse NotFound() => new GeneralResponse(false, nameof(GeneralDepartment)+ConstantsResponse.NotFound);
        private static GeneralResponse Exited() => new GeneralResponse(false, nameof(GeneralDepartment) + ConstantsResponse.Exit);
        private static GeneralResponse HasChild() => new GeneralResponse(false, nameof(GeneralDepartment) + ConstantsResponse.HasChild + "of Branch");
        private static GeneralResponse Success()=> new GeneralResponse(true , ConstantsResponse.Success);

        private async Task<bool> CheckName(string name)
        {
            var item = await _context.GeneralDepartments.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
            return item is null ? false:  true;
        }

    }
}
