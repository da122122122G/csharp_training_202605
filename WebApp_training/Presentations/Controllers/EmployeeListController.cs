using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp_training.Applications.Services;
using WebApp_training.Presentations.ViewModels;

namespace WebApp_training.Presentations.Controllers;

[Route("EmployeeList")]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;


    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("List")]
    public IActionResult List()
    {

        var employees = _employeeService.FindAll();


        var model = employees.Select(e => new EmployeeListItemViewModel
        {
            EmpId = e.Id,
            EmpName = e.Name,
            DeptId = e.Department!.Id,
            DeptName = e.Department.Name,
            PhoneNum = e.PhoneNum,
            EMail = e.EMail
        }).ToList();


        return View(model);
    }

    [HttpPost("Back")]
    public IActionResult Back(EmployeeRegisterViewModel viewModel)
    {
        return RedirectToAction("Enter");
    }
}