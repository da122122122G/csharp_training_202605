using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Applications.Repositories;

public interface IDepartmentRepository
{
    List<Department> FindAll();
    Department? FindById(int id);
    bool ExistsByName(string name);
    void Create(Department department);
}
