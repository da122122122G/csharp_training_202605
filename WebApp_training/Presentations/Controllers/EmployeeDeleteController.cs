using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp_training.Applications.Services;
using WebApp_training.Presentations.ViewModels;
using WebApp_training.Exceptions;
using System.Diagnostics.Eventing.Reader;

namespace WebApp_training.Presentations.Controllers;

[Route("EmployeeDelete")]
public class EmployeeDeleteController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<EmployeeDeleteController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IEmployeeDeleteService _employeeDeleteService;
    /// <summary>
    /// 従業員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly EmployeeDeleteViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<EmployeeDeleteViewModel> _empDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="employeeDeleteService">従業員登録サービスインターフェイス</param>
    /// <param name="employeeDeleteViewModelAdapter">従業員登録ViewModelをEmployeeに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public EmployeeDeleteController(
        ILogger<EmployeeDeleteController> logger,
        IEmployeeDeleteService employeeDeleteService,
        EmployeeDeleteViewModelAdapter employeeDeleteViewModelAdapter,
        TempDataStore<EmployeeDeleteViewModel> empDataStore)
    {
        _logger = logger;
        _employeeDeleteService = employeeDeleteService;
        _adapter = employeeDeleteViewModelAdapter;
        _empDataStore = empDataStore;
    }

    /// <summary>
    /// 従業登録(入力)画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        EmployeeDeleteViewModel? viewModel = null;
        // [戻る]ボタンへの対応
        // TempDataからEmployeeDeleteViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // 従業員登録ViewModelを生成する
            viewModel = new EmployeeDeleteViewModel();
            viewModel.DeptId = 2;
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
    public IActionResult Confirm(EmployeeDeleteViewModel viewModel)
    {
        var employee = _employeeDeleteService.FindById(viewModel.EmpId);
        if (!ModelState.IsValid)
        {
            PopulateDepartments(viewModel);
            return View("Enter", viewModel);
        }
        else if (employee == null)
        {
            ModelState.AddModelError(nameof(viewModel.EmpId), "入力された社員番号は登録されていません");
            // 入力画面の表示
            return View("Enter", viewModel);
        }
        else
        {
            viewModel.EmpName = employee.Name;
            viewModel.DeptName = employee.Department?.Name ?? "未配属";
            viewModel.PhoneNum = employee.PhoneNum;
            viewModel.EMail = employee.EMail;
        }

        return View(viewModel);
    }


    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public IActionResult Delete(EmployeeDeleteViewModel viewModel)
    {
        // EmployeeDeleteViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 登録処理GETアクションメソッドにリダイレクトする
        return RedirectToAction("Complete");
    }

    /// <summary>
    /// アクションメソッド:Regiter()のリダイレクト先
    /// PRGパターン
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        EmployeeDeleteViewModel? viewModel = null;
        // TempDataからEmployeeDeleteViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }
        // EmployeeDeleteFormをドメインモデル:Employeeに変換する
        var employee = _adapter.Restore(viewModel!);
        // 従業員を削除する
        _employeeDeleteService.Delete(employee);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(EmployeeDeleteViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeDeleteViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }

    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(EmployeeDeleteViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var departments = _employeeDeleteService.GetDepartments();
        // 部署一覧をEmployeeDeleteViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部署リストを設定");
    }
}