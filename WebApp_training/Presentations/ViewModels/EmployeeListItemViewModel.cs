using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.ViewModels;

public class EmployeeListItemViewModel
{
    public int EmpId { get; set; }
    public string EmpName { get; set; } = string.Empty;
    public int? DeptId { get; set; } = 0;
    public string? DeptName { get; set; } = string.Empty;
    public string PhoneNum { get; set; } = string.Empty;
    public string EMail { get; set; } = string.Empty;
}