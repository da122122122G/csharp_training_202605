using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp_Exercise.Applications.Domains;
using WebApp_Exercise.Exceptions;
using WebApp_Exercise.Infrastructures.Adapters;
using WebApp_Exercise.Infrastructures.Context;
using WebApp_Exercise.Infrastructures.Entities;
using WebApp_Exercise.Infrastructures.Repositories;
using WebApp_Exercise.TestProject.TestDoubles;

namespace WebApp_Exercise.TestProdect;

[TestClass]
public sealed class DepartmentRepositoryTests
{
    [TestMethod]
    public void FindAll_ReturnsAllDepartment()
    {
        using var context = CreateContext(
        [
            new DepartmentEntity { DeptId = 1, Name = "未所属" },
            new DepartmentEntity { DeptId = 2, Name = "総務部" },
        ]);
        var repository = CreateRepository(context);

        var departments = repository.FindAll();

        Assert.AreEqual(2, departments.Count);
        AssertCategory(departments[0], 1, "未所属");
        AssertCategory(departments[1], 2, "総務部");
    }

    [TestMethod]
    public void FindById_WhenDepartmentExists_ReturnsDepartment()
    {
        using var context = CreateContext(
        [
            new DepartmentEntity { DeptId = 1, Name = "未所属" },
            new DepartmentEntity { DeptId = 2, Name = "総務部" },
        ]);
        var repository = CreateRepository(context);

        var department = repository.FindById(2);

        Assert.IsNotNull(department);
        AssertDepartment(department, 2, "総務部");
    }

    [TestMethod]
    public void FindById_WhenDepartmentDoesNotExist_ReturnsNull()
    {
        using var context = CreateContext(
        [
            new DepartmentEntity { DeptId = 1, Name = "未所属" },
        ]);
        var repository = CreateRepository(context);

        var department = repository.FindById(2);

        Assert.IsNull(department);
    }

    [TestMethod]
    public void FindAll_WhenDbSetThrows_WrapsExceptionInInternalException()
    {
        using var context = CreateContext(new ThrowingDbSet<DepartmentEntity>());
        var repository = CreateRepository(context);

        var exception = Assert.ThrowsException<InternalException>(() => repository.FindAll());

        Assert.IsInstanceOfType<InvalidOperationException>(exception.InnerException);
    }

    private static DepartmentRepository CreateRepository(AppDbContext context)
    {
        return new DepartmentRepository(context, new DepartmentEntityAdapter());
    }

    private static AppDbContext CreateContext(IEnumerable<DepartmentEntity> entities)
    {
        return CreateContext(new QueryableDbSet<DepartmentEntity>(entities));
    }

    private static AppDbContext CreateContext(DbSet<DepartmentEntity> departments)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().Options;
        return new AppDbContext(options)
        {
            Departments = departments,
        };
    }

    private static void AssertDepartment(Department department, int Deptid, string name)
    {
        Assert.AreEqual(DeptId, department.DeptId);
        Assert.AreEqual(DeptName, department.Name);
    }
}
