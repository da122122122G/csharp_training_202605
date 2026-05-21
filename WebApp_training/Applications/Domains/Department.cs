using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Exceptions;

namespace WebApp_training.Applications.Domains
{
    public class Department
    {

        public int? Id { get; private set; }      // 部署Id
        public string? Name { get; private set; } = string.Empty;    // 部署名
        private const int MaxLength = 20; // 部署名の長さ


        public Department(int? id, string? name)
        {
            // 部署名のルール検証
            validateDepartmentName(name);
            this.Id = id;
            this.Name = name;
        }


        public Department(string? name) : this(null, name) { }

        public Department(int? id)
        {
            this.Id = id;
        }


        private void validateDepartmentName(string? name)
        {
            if (name is not null)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new DomainException("部署名は必須です");
                if (name.Length > MaxLength)
                    throw new DomainException($"部署名は{MaxLength}文字以内で入力してください");
            }
        }


        public void ChangeName(string? name)
        {
            // 部署名のルール検証
            validateDepartmentName(name);
            this.Name = name;
        }


        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Department other) return false;
            return Id == other.Id;
        }
        public override int GetHashCode() => Id?.GetHashCode() ?? 0;

        public override string ToString() => $"{Id?.ToString() ?? "未登録"}: {Name}";
    }
}