using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.ViewModels;

public class DepartmentRegisterViewModel
{

    [Display(Name = "部署名")]
    [StringLength(20, ErrorMessage = "{1}文字以内で入力してください。")]
    public string? Name { get; set; } = string.Empty;

    [Display(Name = "部署番号")]
    [Required(ErrorMessage = "{0}は選択必須です。")]
    [Range(1, 99999, ErrorMessage = "5桁以内で入力してください。")]
    public int? DeptId { get; set; } = 0;




    public List<SelectListItem>? Departments { get; set; } = null;

    public override string ToString()
    {
        return $"DeptName={Name} , Departments={Departments} ";
    }
}

