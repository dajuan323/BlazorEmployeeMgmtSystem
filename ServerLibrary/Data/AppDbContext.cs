using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
   
    // General Departments / Department / Branch
    public DbSet<GeneralDepartment> GeneralDepartments { get; set; }    
    public DbSet<Department> Departments { get; set; }
    public DbSet<Branch> Branches { get; set; }
    
    // Country / City / Town
    public DbSet<Town> Towns { get; set; }

    // Auth / Role / System Roles
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }    
    public DbSet<SystemRole> SystemRoles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

    // Other / Veacation / Sanction / Doctor / Overtime
    public DbSet<Vacation> Vacations { get; set; }
    public DbSet<VacationType> VacationTypes { get; set; }
    public DbSet<Overtime> Overtimes { get; set; }
    public DbSet<OverTimeType> OverTimeTypes { get; set; }
    public DbSet<Sanction> Sanctions { get; set; }
    public DbSet<SanctionType> SanctionTypes { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

}
