using ServerLibrary.Data;
using ServerLibrary.Services.Contracts;

namespace ServerLibrary.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        public AccountService(AppDbContext context)
        {
            _context = context?? throw new ArgumentNullException($"Database Null Exception");
        }
        public async Task<T?> AddToDatabase<T>(T? model) where T : class
        {
            if(model==null) throw new ArgumentNullException($"Yor {nameof(model)} model is empty");

            var result =_context.Add(model!);
            await _context.SaveChangesAsync();
            return (T?)result.Entity;
        }
    }
}
