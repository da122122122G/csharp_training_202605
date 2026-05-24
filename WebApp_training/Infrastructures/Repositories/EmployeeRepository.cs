using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Infrastructures.Context;
using WebApp_training.Applications.Domains;
using WebApp_training.Applications.Repositories;
using WebApp_training.Infrastructures.Adapters;
using WebApp_training.Exceptions;


namespace WebApp_training.Infrastructures.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインモデル:従業員と従業員エンティティの相互変換インターフェイスの実装
    /// </summary>
    private readonly EmployeeEntityAdapter _adapter;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context"></param>
    /// <param name="adapter"></param>
    public EmployeeRepository(AppDbContext context, EmployeeEntityAdapter adapter)
    {
        _context = context;
        _adapter = adapter;
    }

    /// <summary>
    /// 従業員を永続化する
    /// </summary>
    /// <param name="employee">永続化対象の従業員</param>
    public void Create(Employee employee)
    {
        try
        {
            var entity = _adapter.Convert(employee);
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InternalException(
                "従業員の永続化ができませんでした。", e);
        }
    }

    public Employee? FindById(int id)
    {
        try
        {
            var result = _context.Employees.FirstOrDefault(e => e.EmpId == id);
            if (result == null)
            {
                return null;
            }
            return _adapter.Restore(result);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "指定された社員Idの社員を取得できませんでした。", e);
        }
    }

    public Employee? FindByName(string name)
    {
        try
        {
            var result = _context.Employees.FirstOrDefault(e => e.EmpName.Contains(name));
            if (result == null)
            {
                return null;
            }
            return _adapter.Restore(result);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "指定された名前を含む社員を取得できませんでした。", e);
        }
    }

    public List<Employee> FindAll()
    {
        try
        {
            var entities = _context.Employees.ToList();
            var results = new List<Employee>();
            foreach (var entity in entities)
            {
                results.Add(_adapter.Restore(entity));
            }
            return results;
        }
        catch (Exception e)
        {
            throw new InternalException(
                "従業員一覧を取得できませんでした。", e);
        }
    }

}