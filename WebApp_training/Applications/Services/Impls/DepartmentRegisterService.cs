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
    public class DepartmentRegisterService : IDepartmentRegisterService
    {
        private readonly AppDbContext _context;
        /// <summary>
        /// ドメインオブジェクト:社員のCRUD操作インターフェイス
        /// </summary>
        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentRegisterService(
    AppDbContext context,
    IDepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
        }

        public void Register(Department department)
        {
            try
            {
                // トランザクションの開始
                _context.Database.BeginTransaction();
                // 社員の登録
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

    }
}