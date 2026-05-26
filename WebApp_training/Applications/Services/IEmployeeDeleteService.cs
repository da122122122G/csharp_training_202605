using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Applications.Services;

public interface IEmployeeDeleteService
{
    List<Department> GetDepartments();
    bool ExistsById(int id);
    Employee FindById(int id);
    Department? GetById(int id);
    void Delete(Employee employee);
}