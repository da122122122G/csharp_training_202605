using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Applications.Repositories;

public interface IEmployeeRepository
{
    void Create(Employee employee);
}
