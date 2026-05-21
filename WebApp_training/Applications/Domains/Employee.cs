using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Exceptions;

namespace WebApp_training.Applications.Domains
{
    public class Employee
    {
        public int? Id { get; private set; } // 社員Id

        public string Name { get; private set; } = string.Empty;// 氏名
        public string PhoneNum { get; private set; } = string.Empty;
        public string EMail { get; private set; } = string.Empty;
        public Department? Department { get; private set; } // 所属部署（null可）

        private const int MaxLength = 20;


        public Employee(int? id, string name, string phoneNum, string eMail, Department? department)
        {
            ValidateName(name);
            Id = id;
            Name = name;
            PhoneNum = phoneNum;
            EMail = eMail;
            Department = department;
        }


        public Employee(string name, string phoneNum, string eMail, Department? department)
            : this(null, name, phoneNum, eMail, department) { }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("氏名は必須です");
            if (name.Length > MaxLength)
                throw new DomainException($"氏名は{MaxLength}文字以内で入力してください");
        }



        public void ChangeName(string name)
        {
            ValidateName(name);
            Name = name;
        }

        public void ChangeDepartment(Department? department)
        {
            Department = department;
        }

        public void ChangePhoneNum(string phoneNum)
        {
            PhoneNum = phoneNum;
        }

        public void ChangeDepartment(string eMail)
        {
            EMail = eMail;
        }


        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Employee other) return false;
            return Id == other.Id;
        }

        public override int GetHashCode() => Id?.GetHashCode() ?? 0;

        public override string ToString()
            => $"{Id?.ToString() ?? "未登録"}: {Name} / {Department?.Name ?? "未配属"}";
    }
}