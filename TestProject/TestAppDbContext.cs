using Microsoft.EntityFrameworkCore;
using WebApp_training.Applications.Domains;
using WebApp_training.Infrastructures.Context;
using WebApp_training.Infrastructures.Entities;

namespace WebApp_training.TestProdect;

internal sealed class TestAppDbContext : AppDbContext
{
    public TestAppDbContext()
        : base(new DbContextOptionsBuilder<AppDbContext>().Options)
    {
        Departments = new QueryableDbSet<DepartmentEntity>([]);
        Employees = new QueryableDbSet<EmployeeEntity>([]);
    }

    public int SaveChangesCallCount { get; private set; }

    public override int SaveChanges()
    {
        SaveChangesCallCount++;
        return 1;
    }
}
