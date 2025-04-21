using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    // Employee class 
    public DbSet<Employee> Employees { get; set; }


    //GeneralDeparment / Deparment / Branch
    public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Branch> Branches { get; set; }


    // Country / City / Town
    public DbSet<Town> Towns { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }


    // AppUser / SystemRole / UserRole / RefreshTokenInfo
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<SystemRole> SystemRoles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshTokenInfo> RefreshTokens { get; set; }


    // OverTime / Vacation / Sanction / Doctor / Other
    public DbSet<OverTime> OverTimes { get; set; }
    public DbSet<OverTimeType> OverTimeTypes { get; set; }

    public DbSet<Vacation> Vacations { get; set; }
    public DbSet<VacationType> VacationTypes { get; set; }

    public DbSet<Sanction> Sanctions { get; set; }
    public DbSet<SanctionType> SanctionTypes { get; set; }

    public DbSet<Doctor> Doctors { get; set; }
}
