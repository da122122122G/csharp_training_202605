using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp_training.Applications.Domains;
using WebApp_training.Exceptions;
using WebApp_training.Infrastructures.Adapters;
using WebApp_training.Infrastructures.Context;
using WebApp_training.Infrastructures.Entities;
using WebApp_training.Infrastructures.Repositories;

namespace WebApp_Exercise.Tests.Infrastructures.Repositories;

[TestClass]
public sealed class DepartmentRepositoryTests
{
    [TestMethod]
    public void FindAll_ReturnsAllDepartment()
    {
        using var context = CreateContext(
        [
            new DepartmentEntity { Id = 1, Name = "Books" },
            new DepartmentEntity { Id = 2, Name = "Games" },
        ]);
        var repository = CreateRepository(context);

        var categories = repository.FindAll();

        Assert.AreEqual(2, categories.Count);
        AssertCategory(categories[0], 1, "Books");
        AssertCategory(categories[1], 2, "Games");
    }

    [TestMethod]
    public void FindById_WhenCategoryExists_ReturnsCategory()
    {
        using var context = CreateContext(
        [
            new ItemCategoryEntity { Id = 1, Name = "Books" },
            new ItemCategoryEntity { Id = 2, Name = "Games" },
        ]);
        var repository = CreateRepository(context);

        var category = repository.FindById(2);

        Assert.IsNotNull(category);
        AssertCategory(category, 2, "Games");
    }

    [TestMethod]
    public void FindById_WhenCategoryDoesNotExist_ReturnsNull()
    {
        using var context = CreateContext(
        [
            new ItemCategoryEntity { Id = 1, Name = "Books" },
        ]);
        var repository = CreateRepository(context);

        var category = repository.FindById(2);

        Assert.IsNull(category);
    }

    [TestMethod]
    public void FindAll_WhenDbSetThrows_WrapsExceptionInInternalException()
    {
        using var context = CreateContext(new ThrowingDbSet<ItemCategoryEntity>());
        var repository = CreateRepository(context);

        var exception = Assert.ThrowsException<InternalException>(() => repository.FindAll());

        Assert.IsInstanceOfType<InvalidOperationException>(exception.InnerException);
    }

    private static ItemCategoryRepository CreateRepository(AppDbContext context)
    {
        return new ItemCategoryRepository(context, new ItemCategoryEntityAdapter());
    }

    private static AppDbContext CreateContext(IEnumerable<ItemCategoryEntity> entities)
    {
        return CreateContext(new QueryableDbSet<ItemCategoryEntity>(entities));
    }

    private static AppDbContext CreateContext(DbSet<ItemCategoryEntity> itemCategories)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().Options;
        return new AppDbContext(options)
        {
            ItemCategories = itemCategories,
        };
    }

    private static void AssertDepartment(Department department, int id, string name)
    {
        Assert.AreEqual(id, department.Id);
        Assert.AreEqual(name, department.Name);
    }

    private sealed class QueryableDbSet<TEntity> : DbSet<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>
        where TEntity : class
    {
        private readonly IQueryable<TEntity> _queryable;

        public QueryableDbSet(IEnumerable<TEntity> entities)
        {
            _queryable = entities.AsQueryable();
        }

        public override IEntityType EntityType => throw new NotSupportedException();

        Type IQueryable.ElementType => _queryable.ElementType;

        Expression IQueryable.Expression => _queryable.Expression;

        IQueryProvider IQueryable.Provider => _queryable.Provider;

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _queryable.GetEnumerator();
        }
    }

    private sealed class ThrowingDbSet<TEntity> : DbSet<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>
        where TEntity : class
    {
        private readonly IQueryable<TEntity> _queryable = Array.Empty<TEntity>().AsQueryable();

        public override IEntityType EntityType => throw new NotSupportedException();

        Type IQueryable.ElementType => _queryable.ElementType;

        Expression IQueryable.Expression => _queryable.Expression;

        IQueryProvider IQueryable.Provider => _queryable.Provider;

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            throw new InvalidOperationException("Database access failed.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new InvalidOperationException("Database access failed.");
        }
    }
}