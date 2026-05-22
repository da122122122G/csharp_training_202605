using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp_training.Applications.Domains;
using WebApp_training.Exceptions;
using WebApp_training.Infrastructures.Adapters;
using WebApp_training.Infrastructures.Context;
using WebApp_training.Infrastructures.Entities;
using WebApp_training.Infrastructures.Repositories;


namespace WebApp_training.TestProdect;

[TestClass]
public sealed class DepartmentRepositoryTests
{
    [TestMethod]
    public void FindAll_ReturnsAllDepartment()
    {
        using var context = CreateContext(
        [
            new DepartmentEntity { DeptId = 1, DeptName = "未所属" },
            new DepartmentEntity { DeptId = 2, DeptName = "総務部" },
        ]);
        var repository = CreateRepository(context);

        var departments = repository.FindAll();

        Assert.AreEqual(2, departments.Count);
        AssertDepartment(departments[0], 1, "未所属");
        AssertDepartment(departments[1], 2, "総務部");
    }

    [TestMethod]
    public void FindById_WhenDepartmentExists_ReturnsDepartment()
    {
        using var context = CreateContext(
        [
            new DepartmentEntity { DeptId = 1, DeptName = "未所属" },
            new DepartmentEntity { DeptId = 2, DeptName = "総務部" },
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
            new DepartmentEntity { DeptId = 1, DeptName = "未所属" },
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

    private static void AssertDepartment(Department department, int id, string name)
    {
        Assert.AreEqual(id, department.Id);
        Assert.AreEqual(name, department.Name);
    }
}
