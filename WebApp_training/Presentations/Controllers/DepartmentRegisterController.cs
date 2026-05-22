using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp_training.Applications.Services;
using WebApp_training.Presentations.ViewModels;

namespace WebApp_training.Presentations.Controllers;

[Route("DepartmentRegister")]
public class DepartmentRegisterController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<DepartmentRegisterController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IDepartmentRegisterService _departmentRegisterService;
    /// <summary>
    /// 従業員登録ViewModelをDepartmentに変換するアダプター
    /// </summary>
    private readonly DepartmentRegisterViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<DepartmentRegisterViewModel> _empDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="departmentRegisterService">従業員登録サービスインターフェイス</param>
    /// <param name="departmentRegisterViewModelAdapter">従業員登録ViewModelをDepartmentに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public DepartmentRegisterController(
        ILogger<DepartmentRegisterController> logger,
        IDepartmentRegisterService departmentRegisterService,
        DepartmentRegisterViewModelAdapter departmentRegisterViewModelAdapter,
        TempDataStore<DepartmentRegisterViewModel> empDataStore)
    {
        _logger = logger;
        _departmentRegisterService = departmentRegisterService;
        _adapter = departmentRegisterViewModelAdapter;
        _empDataStore = empDataStore;
    }

    /// <summary>
    /// 従業登録(入力)画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        DepartmentRegisterViewModel? viewModel = null;
        // [戻る]ボタンへの対応
        // TempDataからDepartmentRegisterViewModelを取得する
        viewModel = _empDataStore.Load(this);
        // viewModelをviewに渡して画面表示する
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(DepartmentRegisterViewModel viewModel)
    {
        // バリデーションチェック
        if (!ModelState.IsValid) // バリデーションエラーあり
        {
            // 入力画面の表示
            return View("Enter", viewModel);
        }
        if (!string.IsNullOrEmpty(viewModel.Name) && _departmentRegisterService.ExistsByName(viewModel.Name) == true) // 入力値あり
        {
            // 入力画面の表示
            return View("Enter", viewModel);
        }
        // 選択された部署のIdで部署データを取得する
        _logger.LogInformation($"部署Id:{viewModel.DeptId ?? 0}の部署を取得する");

        // 確認画面を表示する
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Register")]
    public IActionResult Register(DepartmentRegisterViewModel viewModel)
    {
        // DepartmentRegisterViewModelをシリアライズして、TempDataに保存する
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
        DepartmentRegisterViewModel? viewModel = null;
        // TempDataからDepartmentRegisterViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }
        // DepartmentRegisterFormをドメインモデル:Departmentに変換する
        var department = _adapter.Restore(viewModel!);
        // 新しい従業員を登録する
        _departmentRegisterService.Register(department);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(DepartmentRegisterViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // DepartmentRegisterViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }


}