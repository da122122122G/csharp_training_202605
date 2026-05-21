using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Adapters;
using WebApp_training.Applications.Domains;
using WebApp_training.Infrastructures.Entities;

namespace WebApp_training.Infrastructures.Adapters;

public class EmployeeEntityAdapter :
IConverter<Employee, EmployeeEntity>, IRestorer<Employee, EmployeeEntity>
{

    public EmployeeEntity Convert(Employee domain)
    {
        var entity = new EmployeeEntity
        {
            EmpName = domain.Name
        };
        if (domain.Id != null)
        {
            entity.EmpId = domain.Id.Value;
        }
        if (domain.Department != null)
        {
            entity.DeptId = domain.Department.Id;
        }
        if (domain.PhoneNum != null)
        {
            entity.PhoneNum = domain.PhoneNum;
        }
        if (domain.EMail != null)
        {
            entity.EMail = domain.EMail;
        }
        return entity;
    }


    public Employee Restore(EmployeeEntity target)
    {
        var employee = new Employee(
            target.EmpId,
            target.EmpName,
            target.PhoneNum ?? string.Empty,
            target.EMail ?? string.Empty,
            null

        );
        return employee;
    }
}