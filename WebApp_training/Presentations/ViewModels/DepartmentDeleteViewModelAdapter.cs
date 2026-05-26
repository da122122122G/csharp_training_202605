using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Adapters;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.ViewModels;

public class DepartmentDeleteViewModelAdapter : IRestorer<Department, DepartmentDeleteViewModel>
{
    /// <summary>
    /// DepartmentDeleteViewModelをドメインオブジェクト:Departmentに変換する
    /// </summary>
    /// <param name="target">DepartmentDeleteViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public Department Restore(DepartmentDeleteViewModel target)
    {
        // Department(部署)を作成する
        var department = new Department(target.DeptId, target.Name);
        return department;
    }
}