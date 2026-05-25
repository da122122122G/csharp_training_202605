using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Adapters;
using WebApp_training.Applications.Domains;
using WebApp_training.Infrastructures.Entities;


namespace WebApp_training.Infrastructures.Adapters;

public class DepartmentEntityAdapter :
IConverter<Department, DepartmentEntity>, IRestorer<Department, DepartmentEntity>
{

    public DepartmentEntity Convert(Department domain)
    {
        var entity = new DepartmentEntity
        {
            DeptName = domain.Name!,
        };

        entity.DeptId = domain.Id;

        return entity;
    }

    public Department Restore(DepartmentEntity target)
    {
        var department = new Department(target.DeptId, target.DeptName!);
        return department;
    }
}