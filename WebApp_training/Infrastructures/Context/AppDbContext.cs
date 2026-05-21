using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp_training.Infrastructures.Entities;

namespace WebApp_training.Infrastructures.Context;

public class AppDbContext : DbContext
{
    public DbSet<EmployeeEntity> Employees { get; set; } = null!;
    public DbSet<DepartmentEntity> Departments { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
