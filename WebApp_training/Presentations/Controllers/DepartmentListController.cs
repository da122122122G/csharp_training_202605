using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp_training.Applications.Services;
using WebApp_training.Presentations.ViewModels;

namespace WebApp_training.Presentations.Controllers;

[Route("DepartmentList")]
public class DepartmentListController : Controller
{
    private readonly IDepartmentService _departmentService;


    public DepartmentListController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet("List")]
    public IActionResult List()
    {

        var departments = _departmentService.FindAll();


        var model = departments
            .Where(d => d.Id != 1)
            .Select(d => new DepartmentListItemViewModel
            {
                DeptId = d.Id,
                DeptName = d.Name ?? string.Empty,
            }).ToList();


        return View(model);
    }

    [HttpPost("Back")]
    public IActionResult Back(DepartmentRegisterViewModel viewModel)
    {
        return RedirectToAction("Enter");
    }
}