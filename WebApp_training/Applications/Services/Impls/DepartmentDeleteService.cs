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
    public class DepartmentDeleteService : IDepartmentDeleteService
    {
        private readonly AppDbContext _context;
        /// <summary>
        /// ドメインオブジェクト:従業員のCRUD操作インターフェイス
        /// </summary>
        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentDeleteService(
    AppDbContext context,
    IDepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
        }

        public void Delete(Department department)
        {
            try
            {
                // トランザクションの開始
                _context.Database.BeginTransaction();
                // 従業員の登録
                _departmentRepository.Create(department);
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

        public bool ExistsByName(string name)
        {
            var result = _departmentRepository.ExistsByName(name)!;
            return result;
        }

        public Department GetById(int id)
        {
            var result = _departmentRepository.FindById(id)!;
            if (result == null)
            {
                throw new NotFoundException($"部署Id{id}に該当する部署は存在しません");
            }
            return result;
        }

        public List<Department> GetDepartments()
        {
            return _departmentRepository.FindAll();
        }

        public bool ExistsById(int id)
        {
            var result = _departmentRepository.ExistsById(id)!;
            return result;
        }


    }
}