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
public class EmployeeRepositoryTests0Check
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=cs_db_202605;Username=postgres;Password=training;";

    private EmployeeRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var adapter = new EmployeeEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "SQL", "notFound.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmployeeRepository(_context, adapter);
    }

    [TestMethod]
    public void FindAll_Result_When0()
    {
        var actual = _repository.FindAll();

        AreEqual(0, actual.Count);
    }

}
