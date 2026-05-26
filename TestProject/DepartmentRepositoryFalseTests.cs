
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

public class DepartmentRepositoryFalseTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=cs_db_202605;Username=postgres;Password=training;";

    private DepartmentRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var employeeAdapter = new DepartmentEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .Options;

        _context = new AppDbContext(options);

        _repository = new DepartmentRepository(_context, employeeAdapter);
    }



    [TestMethod]
    public void Create_WhenError()
    {
        var department = new Department(2, "総務部");

        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.Create(department);
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
    public void FindById_Result_WhenError()
    {
        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindById(2);
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
    public void ExistsByName_WhenError()
    {
        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.ExistsByName("田中");
        });
    }

    [TestMethod]
    public void Delete_WhenError()
    {
        var department = new Department(2, "総務部");

        var exception = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.Delete(department);
        });
    }

    [TestMethod]
    public void Update_WhenError()
    {
        int targetEmpId = 3;
        var newDepartment = new Department(2, "総務部");
        var departmentToUpdate = new Department(
            targetEmpId,
            "変更後の検証用氏名"
        );

        var exception = Assert.ThrowsException<InternalException>(() =>
        {

            _repository.Update(departmentToUpdate);
        });
    }
}