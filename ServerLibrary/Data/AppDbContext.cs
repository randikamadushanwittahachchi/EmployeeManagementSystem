using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Town> Towns { get; set; }
    public DbSet<SystemRole> SystemRoles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshTokenInfo> RefreshTokens { get; set; }
}
