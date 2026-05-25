using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using WebApp_training.Applications.Domains;
using WebApp_training.Infrastructures.Adapters;
using WebApp_training.Infrastructures.Context;
using WebApp_training.Infrastructures.Repositories;

namespace WebApp_training.Test.Infrastructures.Repositories;

[DoNotParallelize]
[TestClass]
public class DepartmentRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=cs_db_202605;Username=postgres;Password=training;";

    private DepartmentRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var adapter = new DepartmentEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "SQL", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new DepartmentRepository(_context, adapter);
    }

    [TestMethod]
    public void FindAll_Result()
    {
        var actual = _repository.FindAll();

        AreEqual(6, actual.Count);
        IsTrue(actual.Any(d => d.Equals(new Department(1, "未所属"))));
        IsTrue(actual.Any(d => d.Equals(new Department(2, "総務部"))));
        IsTrue(actual.Any(d => d.Equals(new Department(3, "経理部"))));
        IsTrue(actual.Any(d => d.Equals(new Department(4, "人事部"))));
        IsTrue(actual.Any(d => d.Equals(new Department(5, "開発部"))));
        IsTrue(actual.Any(d => d.Equals(new Department(6, "営業部"))));
    }

    [TestMethod]
    public void FindById_WhenIdCorrect()
    {
        var expected = new Department(2, "総務部");
        var actual = _repository.FindById(2);

        AreEqual(expected, actual);
        AreEqual("総務部", actual?.Name);
    }

    [TestMethod]
    public void FindById_WhenIdNotFound()
    {
        var actual = _repository.FindById(999);
        IsNull(actual);
    }

    [TestMethod]
    public void Create_WhenCorrect()
    {
        var beforeCount = _context.Departments.Count();

        var department = new Department(2, "人事部");

        _repository.Create(department);

        var afterCount = _context.Departments.Count();
        AreEqual(beforeCount + 1, afterCount);

        var created = _context.Departments
            .FirstOrDefault(i => i.DeptName == "人事部");

        IsNotNull(created);
        IsNotNull(created.DeptId);
        AreEqual("人事部", created.DeptName);
    }

    [TestMethod]
    public void ExistsByName_WhenNameExists()
    {
        var actual = _repository.ExistsByName("総務部");
        IsTrue(actual);
    }

    [TestMethod]
    public void ExistsByName_WhenNameNotExists()
    {
        var actual = _repository.ExistsByName("帰宅部");
        IsFalse(actual);
    }

}
