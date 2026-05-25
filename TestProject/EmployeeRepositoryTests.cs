
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
public class EmployeeRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=cs_db_202605;Username=postgres;Password=training;";

    private EmployeeRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var employeeAdapter = new EmployeeEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "SQL", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmployeeRepository(_context, employeeAdapter);
    }


    [TestMethod]
    public void FindById_WhenIdCorrect()
    {
        var actual = _repository.FindById(1);

        IsNotNull(actual);
        AreEqual(1, actual.Id);
        AreEqual("田中太郎", actual.Name);
        AreEqual("012-3456-7890", actual.PhoneNum);
        AreEqual("afdbv@awerv", actual.EMail);
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
        var beforeCount = _context.Employees.Count();

        var department = new Department(2, "総務部");
        var employee = new Employee(0, "検証用氏名", "345-6789-0123", "natrfk@katsmr", department);
        employee.ChangeDepartment(department);

        _repository.Create(employee);

        var afterCount = _context.Employees.Count();
        AreEqual(beforeCount + 1, afterCount);

        var created = _context.Employees
            .FirstOrDefault(i => i.EmpName == "検証用氏名");

        IsNotNull(created);
        IsNotNull(created.DeptId);
    }

    [TestMethod]
    public void FindByName_WhenNameCorrect()
    {
        var actual = _repository.FindByName("田中");

        IsNotNull(actual);
        AreEqual(1, actual.Id);
        AreEqual("田中太郎", actual.Name);
        AreEqual("012-3456-7890", actual.PhoneNum);
        AreEqual("afdbv@awerv", actual.EMail);
    }

    [TestMethod]
    public void FindByName_WhenNameNotFound()
    {
        var actual = _repository.FindByName("四月朔日");
        IsNull(actual);
    }

    [TestMethod]
    public void FindAll_Result()
    {
        var actual = _repository.FindAll();

        AreEqual(5, actual.Count);
        IsTrue(actual.Any(d => d.Equals(new Employee(1, "田中太郎", "012-3456-7890", "afdbv@awerv", null))));
        IsTrue(actual.Any(d => d.Equals(new Employee(2, "鈴木三郎", "123-4567-8901", "okjnbg@okjh", null))));
        IsTrue(actual.Any(d => d.Equals(new Employee(3, "佐藤花子", "234-5678-9012", "aeruvn@kerun", null))));
        IsTrue(actual.Any(d => d.Equals(new Employee(4, "中田彩子", "345-6789-0123", "ureid@cxmvae", null))));
        IsTrue(actual.Any(d => d.Equals(new Employee(5, "加藤圭太", "45-6789-0123", "asdgv@qer", null))));

    }

}
