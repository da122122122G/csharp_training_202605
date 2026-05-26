using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Applications.Domains;

namespace WebApp_training.Applications.Services;

public interface IDepartmentService
{
    /// <summary>
    /// 全ての従業員を取得する
    /// </summary>
    /// <returns>従業員のリスト</returns>
    List<Department> FindAll();
}