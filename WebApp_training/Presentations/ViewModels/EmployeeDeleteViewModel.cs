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
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [Range(1, 99999, ErrorMessage = "{1}~{2}の範囲で入力してください。")]
    public int? EmpId { get; set; } = 1;
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

    public void SetDepartments(List<Department> departments)
    {
        // SelectListItemのリストを作成
        var selectItems = new List<SelectListItem>();
        foreach (var dept in departments)
        {

            var item = new SelectListItem();
            item.Value = dept.Id.ToString();
            item.Text = string.IsNullOrEmpty(dept.Name) ? "(名称未設定)" : dept.Name;
            selectItems.Add(item);

        }
        Departments = selectItems;
    }

    public List<SelectListItem>? Departments { get; set; } = null;
    public override string ToString()
    {
        return $"Name={EmpName} , DeptId={DeptId} , DeptName={DeptName} ,  PhoneNum={PhoneNum} , EMail={EMail}";
    }

}
