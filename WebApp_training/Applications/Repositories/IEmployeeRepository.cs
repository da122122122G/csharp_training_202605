using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Applications.Repositories;

public interface IEmployeeRepository
{
    void Create(Employee employee);
    List<Employee> FindAll();
    void Delete(Employee employee);
    void Update(Employee employee);
    Employee? FindById(int id);
    Employee? FindByName(string name);
    bool ExistsById(int id);
    List<Employee> GetEmpsByDeptId(int deptId);
}
