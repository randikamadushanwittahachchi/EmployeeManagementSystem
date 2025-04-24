using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositores.Contracts;

namespace ServerLibrary.Repositores.Implementations
{
    public class GeneralDepartmentRepository : IGenericRepositoryInterface<GeneralDepartment>
    {

        private readonly AppDbContext _context;

        public GeneralDepartmentRepository(AppDbContext? context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context),"Database connection error");
        }


        public async Task<List<GeneralDepartment>> GetAll() => await _context.GeneralDepartments.ToListAsync();
        public async Task<GeneralDepartment?> GetById(int id) => await FindById(id);

        public async Task<GeneralResponse> Create(GeneralDepartment model)
        {
            if(await CheckName(model.Name)) return new GeneralResponse(false, $"{model.Name} is already exist");
            _context.GeneralDepartments.Add(model);
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> Update(GeneralDepartment model)
        {
            var item = await FindById(model.Id);
            if (item is null) return NotFound();
            item.Name = model.Name;
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await FindById(id);
            if (item is null) return NotFound();
            _context.GeneralDepartments.Remove(item);
            await Commit();
            return Success();
        }



        // reuseble propety and methode
        private async Task Commit() =>await _context.SaveChangesAsync();
        private async Task<GeneralDepartment?> FindById(int id) => await _context.GeneralDepartments.FindAsync(id);
        private static GeneralResponse NotFound() => new GeneralResponse(false, $"{nameof(GeneralDepartment)} not found");
        private static GeneralResponse Success()=> new GeneralResponse(true , "Processe was Successfull");

        private async Task<bool> CheckName(string name)
        {
            var item = await _context.GeneralDepartments.FirstOrDefaultAsync(_ => string.Equals(_.Name,name,StringComparison.OrdinalIgnoreCase));
            return item == null ? false:  true;
        }

    }
}
