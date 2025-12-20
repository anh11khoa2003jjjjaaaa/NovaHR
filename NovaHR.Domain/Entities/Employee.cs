using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NovaHR.Domain.Entities
{
    public class Employee : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // BaseEntity đã có Id
        public string Code { get; private set; } = null!;

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string FullName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public Gender Gender { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime JoinDate { get; private set; }
        public string EducationLevel { get; private set; } = null!;

        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public Guid DepartmentId { get; private set; }
        public Guid PositionId { get; private set; }
        public Position Position { get; private set; } = null!;
        public Department Department { get; private set; } = null!;
        public ICollection<EmployeeSalary> EmployeeSalaries { get; private set; } = new List<EmployeeSalary>();

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        public EmployeeStatus EmployeeStatus { get; private set; } = EmployeeStatus.Active;
        

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------

        private Employee() { } // EF dùng

        public Employee(string fullname, string email, Gender gender, DateTime dateofbirth, DateTime joindate)
        {
            ValidateBasic(fullname, email, gender, dateofbirth, joindate);

            Code = GenerateCodeEmployee(fullname, joindate);

            SetFullName(fullname);
            Email = email;
            Gender = gender;
            DateOfBirth = dateofbirth;
            JoinDate = joindate;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        private string GenerateCodeEmployee(string fullname, DateTime joindate)
        {
            string initials = string.Join("", fullname
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(a => char.ToUpper(a[0])));

            string date = $"{joindate.Day}{joindate.Month}";
            
            // [FIXED] Dùng Random Number thay vì static int
            var randomPart = new Random().Next(1000, 9999); 
            string code = $"{initials}{date}{randomPart}";

            return code;
        }

        private void ValidateBasic(string fullname, string email, Gender gender, DateTime dob, DateTime joinDate)
        {
            if (string.IsNullOrWhiteSpace(fullname)) throw new DomainException("FullName is required");
            if (!email.Contains("@")) throw new DomainException("Email is invalid");
            if (dob > DateTime.UtcNow) throw new DomainException("Date of birth is invalid.");
            if (joinDate < dob) throw new DomainException("Join date cannot be before date of birth");
            if (!Enum.IsDefined(typeof(Gender), gender)) throw new DomainException("Invalid gender");
        }

        private void SetFullName(string fullname)
        {
            if (string.IsNullOrWhiteSpace(fullname)) throw new DomainException("FullName is not empty!");
            FullName = fullname.Trim();
            Touch(UpdatedBy ?? Guid.Empty);
        }

        
    }
}

