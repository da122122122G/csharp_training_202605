using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp_training.Applications.Services;
using WebApp_training.Presentations.ViewModels;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Presentations.Controllers;

[Route("DepartmentDelete")]
public class DepartmentDeleteController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<DepartmentDeleteController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IDepartmentDeleteService _departmentDeleteService;
    /// <summary>
    /// 従業員登録ViewModelをDepartmentに変換するアダプター
    /// </summary>
    private readonly DepartmentDeleteViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<DepartmentDeleteViewModel> _empDataStore;
    private readonly IEmployeeUpdateService _employeeService;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="DepartmentDeleteService">従業員登録サービスインターフェイス</param>
    /// <param name="DepartmentDeleteViewModelAdapter">従業員登録ViewModelをDepartmentに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public DepartmentDeleteController(
        ILogger<DepartmentDeleteController> logger,
        IDepartmentDeleteService departmentDeleteService,
        DepartmentDeleteViewModelAdapter departmentDeleteViewModelAdapter,
        TempDataStore<DepartmentDeleteViewModel> empDataStore,
        IEmployeeUpdateService employeeService)
    {
        _logger = logger;
        _departmentDeleteService = departmentDeleteService;
        _adapter = departmentDeleteViewModelAdapter;
        _empDataStore = empDataStore;
        _employeeService = employeeService;
    }



    /// <summary>
    /// 従業登録(入力)画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        DepartmentDeleteViewModel? viewModel = null;
        // [戻る]ボタンへの対応
        // TempDataからDepartmentDeleteViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            viewModel = new DepartmentDeleteViewModel();
        }
        // 部署一覧を取得してViewModelに設定する(SelectListItem形式)
        PopulateDepartments(viewModel);
        // viewModelをviewに渡して画面表示する
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(DepartmentDeleteViewModel viewModel)
    {
        var department = _departmentDeleteService.FindById(viewModel.DeptId);
        if (!ModelState.IsValid)
        {
            PopulateDepartments(viewModel);
            return View("Enter", viewModel);
        }

        else if (department == null)
        {
            ModelState.AddModelError(nameof(viewModel.DeptId), "入力された社員番号は登録されていません");
            // 入力画面の表示
            return View("Enter", viewModel);
        }

        if (viewModel.DeptId == 1)
        {
            ModelState.AddModelError(nameof(viewModel.DeptId), "1は削除できません");
            // 入力画面の表示
            return View("Enter", viewModel);
        }

        //bool hasEmployees = _employeeService.HasEmployeesInDepartment(viewModel.DeptId);
        //viewModel.HasRelatedEmployees = hasEmployees;

        _logger.LogInformation($"部署Id:{viewModel.DeptId} の削除確認画面を表示します。");

        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public IActionResult Delete(DepartmentDeleteViewModel viewModel)
    {
        try
        {
            var employeesInDept = _employeeService.GetEmpsByDeptId(viewModel.DeptId);

            if (employeesInDept.Any())
            {
                var defaultDepartment = new Department(1, "未所属");
                foreach (var emp in employeesInDept)
                {
                    emp.ChangeDepartment(defaultDepartment);
                    _employeeService.Update(emp);
                }
                _logger.LogInformation($"部署Id:{viewModel.DeptId} に所属していた {employeesInDept.Count()} 名の社員を部署Id:1に移動しました。");
            }

            var department = _adapter.Restore(viewModel);
            _departmentDeleteService.Delete(department);
            _logger.LogInformation($"部署Id:{viewModel.DeptId} を削除しました。");
            TempData["DeletedDeptName"] = viewModel.Name;
            return RedirectToAction("Complete");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "部署の削除処理中にエラーが発生しました。");
            ModelState.AddModelError(string.Empty, "削除処理に失敗しました。");
            return View("Confirm", viewModel);
        }
    }

    /// <summary>
    /// アクションメソッド:Regiter()のリダイレクト先
    /// PRGパターン
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        ViewBag.DeletedDeptName = TempData["DeletedDeptName"] as string;
        return View();
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(DepartmentDeleteViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // DepartmentDeleteViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }

    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(DepartmentDeleteViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var departments = _departmentDeleteService.FindAll();
        var filteredDepartments = departments.Where(d => d.Id != 1).ToList();
        // 部署一覧をDepartmentDeleteViewModelに登録する
        viewModel.SetDepartments(filteredDepartments);
        _logger.LogInformation("部署ID:1を除外した部署リストを設定");
    }
}