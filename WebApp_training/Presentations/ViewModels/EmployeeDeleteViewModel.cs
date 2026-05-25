using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.ViewModels;

public class EmployeeDeleteViewModel
{

    [Display(Name = "社員番号")]
    public int EmpId { get; set; }
    [Display(Name = "氏名")]
    public string EmpName { get; set; } = string.Empty;
    [Display(Name = "部署ID")]
    public int? DeptId { get; set; } = 0;
    [Display(Name = "部署名")]
    public string? DeptName { get; set; } = string.Empty;
    [Display(Name = "電話番号")]
    public string PhoneNum { get; set; } = string.Empty;
    [Display(Name = "メールアドレス")]
    public string EMail { get; set; } = string.Empty;


    public override string ToString()
    {
        return $"Name={EmpName} , DeptId={DeptId} , DeptName={DeptName} , Departments={Departments} , PhoneNum={PhoneNum} , EMail={EMail}";
    }

}
