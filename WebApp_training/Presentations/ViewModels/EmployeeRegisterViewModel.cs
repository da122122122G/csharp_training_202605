using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.ViewModels;

public class EmployeeRegisterViewModel
{

    [Display(Name = "氏名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [StringLength(20, ErrorMessage = "{1}文字以内で入力してください。")]
    public string? Name { get; set; } = string.Empty;

    [Display(Name = "所属部署")]
    [Required(ErrorMessage = "{0}は選択必須です。")]
    [StringLength(20, ErrorMessage = "{1}文字以内で入力してください。")]
    public int? DeptId { get; set; } = 0;

    [Display(Name = "部署名")]
    [StringLength(20, ErrorMessage = "{1}文字以内で入力してください。")]
    public string? DeptName { get; set; } = string.Empty;

    [Display(Name = "電話番号")]
    [RegularExpression(@"^0\d{1,4}-\d{1,4}-\d{4}$", ErrorMessage = "電話番号の形式（例: 03-1234-5678）で入力してください。")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? PhoneNum { get; set; } = string.Empty;

    [Display(Name = "メールアドレス")]
    [EmailAddress(ErrorMessage = "{0}の形式で入力してください")]
    [StringLength(50, ErrorMessage = "{1}文字以内で入力してください。")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? EMail { get; set; } = string.Empty;

    public void SetDepartments(List<Department> departments)
    {
        // SelectListItemのリストを作成
        var selectItems = new List<SelectListItem>();
        foreach (var dept in departments)
        {
            if (dept.Id.HasValue)
            {
                var item = new SelectListItem();
                item.Value = dept.Id.Value.ToString();
                item.Text = string.IsNullOrEmpty(dept.Name) ? "(名称未設定)" : dept.Name;
                selectItems.Add(item);
            }
        }
        Departments = selectItems;
    }

    public List<SelectListItem>? Departments { get; set; } = null;

    public override string ToString()
    {
        return $"Name={Name} , DeptId={DeptId} , DeptName={DeptName} , Departments={Departments} , PhoneNum={PhoneNum} , EMail={EMail}";
    }
}

