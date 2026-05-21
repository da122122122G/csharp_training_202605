using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_training.Presentations.Controllers;

public class TempDataStore<T> where T : class
{
    /// <summary>
    /// オブジェクトにアクセスするキー
    /// </summary>
    private readonly string _key;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="key">オブジェクトにアクセスするキー</param> <summary>
    public TempDataStore(string key)
    {
        _key = key;
    }

    /// TempDataに保存されたJSONを復元して、指定した型のオブジェクトを返す
    /// </summary>
    /// <param name="controller">TempDataを持つコントローラ</param>
    /// <returns>復元されたオブジェクト、存在しない場合や復元に失敗した場合はnull</returns>
    public T? Load(Controller controller)
    {
        // TempDataにキーが存在するか確認
        if (!controller.TempData.ContainsKey(_key))
        {
            return null;
        }
        // 値を文字列として取得
        string? json = controller.TempData[_key] as string;
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }
        try
        {
            // JSONをオブジェクトに変換して返す
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (JsonException)
        {
            // JSONの形式が不正な場合はnullを返す
            return null;
        }
    }

    /// <summary>
    /// 指定されたコントローラのTempDataに、オブジェクトをJSONとして保存する
    /// </summary>
    /// <param name="controller">TempDataを持つコントローラ</param>
    /// <param name="model">保存するオブジェクト</param>
    public void Save(Controller controller, T model)
    {
        string json = JsonSerializer.Serialize(model);
        controller.TempData[_key] = json;
    }
}