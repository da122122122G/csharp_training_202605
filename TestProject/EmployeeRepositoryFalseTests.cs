
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using WebApp_training.Applications.Domains;
using WebApp_training.Infrastructures.Adapters;
using WebApp_training.Infrastructures.Context;
using WebApp_training.Infrastructures.Repositories;
using WebApp_training.Exceptions;

namespace WebApp_training.Test.Infrastructures.Repositories;

[DoNotParallelize]
[TestClass]
public class EmployeeRepositoryFalseTests
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
            .Options;

        _context = new AppDbContext(options);

        _repository = new EmployeeRepository(_context, employeeAdapter);
    }


    [TestMethod]
    public void FindById_WhenError()
    {
        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindById(1);
        });
    }


    [TestMethod]
    public void Create_WhenError()
    {
        var department = new Department(2, "総務部");
        var employee = new Employee(0, "検証用氏名", "345-6789-0123", "natrfk@katsmr", department);
        employee.ChangeDepartment(department);

        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.Create(employee);
        });
    }

    [TestMethod]
    public void FindByName_WhenError()
    {
        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindByName("田中");
        });
    }


    [TestMethod]
    public void FindAll_Result_WhenError()
    {
        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindAll();
        });
    }


    [TestMethod]
    public void ExistsById_WhenError()
    {
        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.ExistsById(2);
        });
    }


    [TestMethod]
    public void Delete_WhenError()
    {
        var department = new Department(2, "総務部");
        var employee = new Employee(1, "検証用氏名", "345-6789-0123", "natrfk@katsmr", department);
        employee.ChangeDepartment(department);


        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.Delete(employee);
        });
    }

    [TestMethod]
    public void Update_WhenError()
    {
        int targetEmpId = 3;
        var newDepartment = new Department(2, "総務部");
        var employeeToUpdate = new Employee(
            targetEmpId,
            "変更後の検証用氏名",
            "345-6789-0123",
            "natrfk@katsmr",
            newDepartment
        );



        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.Update(employeeToUpdate);
        });
    }

}
