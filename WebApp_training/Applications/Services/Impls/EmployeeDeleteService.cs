using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Repositories;
using WebApp_training.Applications.Domains;
using WebApp_training.Exceptions;
using WebApp_training.Infrastructures.Context;

namespace WebApp_training.Applications.Services.Impls
{
    public class EmployeeDeleteService : IEmployeeDeleteService
    {
        private readonly AppDbContext _context;
        /// <summary>
        /// ドメインオブジェクト:従業員のCRUD操作インターフェイス
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;


        public EmployeeDeleteService(
    AppDbContext context,
    IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public List<Department> GetDepartments()
        {
            return _departmentRepository.FindAll();
        }
        public void Delete(Employee employee)
        {
            try
            {
                // トランザクションの開始
                _context.Database.BeginTransaction();
                // 従業員の登録
                _employeeRepository.Delete(employee);
                // トランザクションのコミット
                _context.Database.CommitTransaction();
            }
            catch
            {
                // トランザクションのロールバック
                _context.Database.RollbackTransaction();
                throw;
            }
        }

        public bool ExistsById(int id)
        {
            var result = _employeeRepository.ExistsById(id)!;
            return result;
        }

        public Employee FindById(int id)
        {
            var result = _employeeRepository.FindById(id)!;
            return result;
        }

        public Department GetById(int id)
        {
            var result = _departmentRepository.FindById(id);
            if (result == null)
            {
                throw new NotFoundException($"部署Id{id}に該当する部署は存在しません");
            }
            return result;
        }

    }
}