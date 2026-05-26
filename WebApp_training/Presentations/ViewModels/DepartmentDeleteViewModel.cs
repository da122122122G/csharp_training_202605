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

    [Display(Name = "部署")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [Range(2, 99999, ErrorMessage = "{1}~{2}の範囲で入力してください。")]
    public int DeptId { get; set; } = 2;


    public List<SelectListItem>? Departments { get; set; } = null;
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

    public override string ToString()
    {
        return $"DeptName={Name} , Departments={Departments} ";
    }
}

