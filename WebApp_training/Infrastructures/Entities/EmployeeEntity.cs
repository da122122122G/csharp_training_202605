using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp_training.Infrastructures.Entities;

[Table("employee")]
public class EmployeeEntity
{
    [Key]
    [Column("id")]
    public int EmpId { get; set; }

    [Column("name")]
    public string EmpName { get; set; } = string.Empty;

    [Column("dept_id")]
    public int? DeptId { get; set; }

    [Column("phone_number")]
    public string? PhoneNum { get; set; }

    [Column("e_mail")]
    public string? EMail { get; set; }
}
