using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Domains;
using WebApp_training.Infrastructures.Entities;

namespace WebApp_training.Presentations.ViewModels
{
    public class DepartmentListItemViewModelAdapter
    {
        public List<DepartmentEntity> Restore(DepartmentListItemViewModel target)
        {
            var department = new DepartmentEntity
            {
                DeptId = target.DeptId,
                DeptName = target.DeptName
            };

            return new List<DepartmentEntity> { department };
        }
    }
}