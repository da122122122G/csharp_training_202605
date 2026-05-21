using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_training.Applications.Repositries;

public interface IDepartmentRepository
{
    List<Department> FindAll();
    Department? FindById(int id);
}
