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

        public async Task<List<Department>> GetAll() => await _context.Departments.ToListAsync();

        public async Task<Department?> GetById(int id) => await FindById(id);

        public async Task<GeneralResponse> Create(Department model)
        {
            if (await CheckName(model.Name)) return new GeneralResponse(false, nameof(Department) + ConstantsResponse.Exit);
            _context.Departments.Add(model);
            await Commite();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department model)
        {
            var item = await FindById(model.Id);
            if (item is null ) return NotFound();
            item.Name = model.Name;
            await Commite();
            return Success();
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await FindById(id);
            if (item is null) return NotFound();
            _context.Departments.Remove(item);
            await Commite();
            return Success();
        }


        // reuseble propety and methode
        private  async Task<Department?> FindById(int id) => await _context.Departments.FindAsync(id);
        private static GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
        private static GeneralResponse NotFound() => new GeneralResponse(false, nameof(Department) + ConstantsResponse.NotFound);
        private async Task Commite() => await _context.SaveChangesAsync();
        private async Task<bool> CheckName(string name)
        {
            var item = await _context.Departments.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
            return item is null ? false : true;
        }

    }
}
