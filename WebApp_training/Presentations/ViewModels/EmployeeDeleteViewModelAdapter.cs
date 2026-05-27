using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Adapters;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.ViewModels;

public class EmployeeDeleteViewModelAdapter : IRestorer<Employee, EmployeeDeleteViewModel>
{
    /// <summary>
    /// EmployeeDeleteViewModelをドメインオブジェクト:Employeeに変換する
    /// </summary>
    /// <param name="target">EmployeeDeleteViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public Employee Restore(EmployeeDeleteViewModel target)
    {
        // Department(部署)を作成する
        var department = new Department(target.DeptId!.Value, target.DeptName);
        // 削除するEmployee(従業員)を作成する
        var employee = new Employee(target.EmpId!.Value, target.EmpName!, target.PhoneNum!, target.EMail!, department);
        return employee;
    }
}