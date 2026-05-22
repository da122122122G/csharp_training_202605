using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Applications.Services
{
    public interface IDepartmentRegisterService
    {
        void Register(Department department);
        bool ExistsByName(string name);
    }
}