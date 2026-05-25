using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.ViewModels;

public class DepartmentDeleteViewModel
{

    [Display(Name = "部署名")]
    public string? Name { get; set; } = string.Empty;

    [Display(Name = "所属部署")]
    public int? DeptId { get; set; } = 0;


    public List<SelectListItem>? Departments { get; set; } = null;

    public override string ToString()
    {
        return $"DeptName={Name} , Departments={Departments} ";
    }
}

