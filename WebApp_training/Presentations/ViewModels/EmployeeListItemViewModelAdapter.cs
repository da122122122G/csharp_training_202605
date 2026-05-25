using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Services.Impls;
using WebApp_training.Infrastructures.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.ViewModels
{
    public class EmployeeListItemViewModelAdapter
    {
        public List<EmployeeEntity> Restore(EmployeeListItemViewModel target)
        {
            var employee = new EmployeeEntity
            {
                EmpId = target.EmpId,
                EmpName = target.EmpName,
                DeptId = target.DeptId,
                PhoneNum = target.PhoneNum,
                EMail = target.EMail
            };

            return new List<EmployeeEntity> { employee };
        }
    }
}