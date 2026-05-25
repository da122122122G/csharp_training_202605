using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Repositories;
using WebApp_training.Applications.Domains;
using WebApp_training.Exceptions;
using WebApp_training.Infrastructures.Context;

namespace WebApp_training.Applications.Services.Impls;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    // コンストラクタでリポジトリをDI（依存性の注入）で受け取る
    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _repository = departmentRepository;
    }

    public List<Department> FindAll()
    {
        // リポジトリに処理を委譲して全件取得する
        return _repository.FindAll();
    }
}