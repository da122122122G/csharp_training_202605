using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp_training.Applications.Domains;
using WebApp_training.Exceptions;
using WebApp_training.Infrastructures.Adapters;
using WebApp_training.Infrastructures.Context;
using WebApp_training.Infrastructures.Entities;
using WebApp_training.Infrastructures.Repositories;


namespace WebApp_training.TestProdect;

[TestClass]
public sealed class EmployeeRepositoryTests
{
    [TestMethod]
    public void Create_Employee_AddsEntitiesAndSavesTwice()
    {
        using var context = CreateContext(Array.Empty<EmployeeEntity>());
        var repository = CreateRepository(context);
        var employee = new Employee(1, "Laptop", "070-1234-5678", "a@b", null);

        repository.Create(employee);

        var savedItems = ((QueryableDbSet<EmployeeEntity>)context.Employees).Entities;
        Assert.AreEqual(1, savedItems.Count);
        var savedItem = savedItems[0];
        Assert.AreEqual(7, savedItem.EmpId);
        Assert.AreEqual("Laptop", savedItem.EmpName);
        Assert.AreEqual("070-1234-5678", savedItem.PhoneNum);
        Assert.AreEqual("a@b", savedItem.EMail);

    }


    [TestMethod]
    public void Create_WhenDbSetThrows_WrapsExceptionInInternalException()
    {
        using var context = CreateContext(new ThrowingDbSet<EmployeeEntity>());
        var repository = CreateRepository(context);

        var exception = Assert.ThrowsException<InternalException>(
            () => repository.Create(new Employee(1, "Laptop", "070-1234-5678", "a@b", null)));

        Assert.IsInstanceOfType<InvalidOperationException>(exception.InnerException);
    }

    private static EmployeeRepository CreateRepository(AppDbContext context)
    {
        return new EmployeeRepository(context, new EmployeeEntityAdapter());
    }

    private static AppDbContext CreateContext(IEnumerable<EmployeeEntity> employees)
    {
        return CreateContext(employees);
    }


}
