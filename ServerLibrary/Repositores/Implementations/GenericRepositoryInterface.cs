using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositores.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace ServerLibrary.Repositores.Implementations
{
    public class GenericRepositoryInterface<T> : IGenericRepositoryInterface<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepositoryInterface(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException($"Database Null Exception");
            _dbSet = _context.Set<T>();
        }


        public Task<List<T>> GetAll()
        {
            return  _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<GeneralResponse> Create(T model)
        {
            _dbSet.Add(model);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Created Successfully");
        }

        public async Task<GeneralResponse> Update(T model)
        {
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Updated Successfully");
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var result = await _dbSet.FindAsync(id);
            if(result == null) return new GeneralResponse(false, "Not Found");
            _dbSet.Remove(result);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Deleted Successfully");
        }
    }
}
