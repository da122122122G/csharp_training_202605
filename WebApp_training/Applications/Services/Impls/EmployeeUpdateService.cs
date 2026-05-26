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
    public class EmployeeUpdateService : IEmployeeUpdateService
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeUpdateService(
            AppDbContext context,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public List<Employee> GetEmpsByDeptId(int deptId)
        {
            return _employeeRepository.GetEmpsByDeptId(deptId);
        }
        public void Update(Employee employee)
        {
            try
            {
                // トランザクションの開始
                _context.Database.BeginTransaction();
                // 従業員の登録
                _employeeRepository.Update(employee);
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
    }
}