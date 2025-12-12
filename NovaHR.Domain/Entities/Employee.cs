using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NovaHR.Domain.Entities
{
    public class Employee
    {
        // Quy tắc 1: Entity
        public Guid Id { get; private set; }
        public string Code { get; private set; } = null!;

        // Quy tắc 2: Mô tả bản chất của entity
        public string FullName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public Gender Gender { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime JoinDate { get; private set; }
        public string EducationLevel { get; private set; } = null!;

        // Quy tắc 3: Quan hệ
        public Guid DepartmentId { get; private set; }
        public Guid PositionId { get; private set; }
        public Position Position { get; private set; } = null!;
        public Department Department { get; private set; } = null!;
        public List<EmployeeSalary> EmployeeSalaries { get; private set; } = new();

        // Quy tắc 4: Thuộc tính thuộc domain business
        public EmployeeStatus EmployeeStatus { get; private set; } = EmployeeStatus.Active;

        // Quy tắc 5: Thuộc tính hệ thống
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public Guid CreatedBy { get; private set; }
        public Guid UpdatedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        private static int _lastDigit = 1;

        private Employee() { } // EF dùng

        public Employee(string fullname, string email, Gender gender, DateTime dateofbirth, DateTime joindate)
        {
            ValidateBasic(fullname, email, gender, dateofbirth, joindate);

            Id = Guid.NewGuid();
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
            string code = $"{initials}{date}{_lastDigit}";

            _lastDigit++;

            return code;
        }

        private void ValidateBasic(string fullname, string email, Gender gender, DateTime dob, DateTime joinDate)
        {
            if (string.IsNullOrWhiteSpace(fullname))
                throw new DomainException("FullName is required");

            if (!email.Contains("@"))
                throw new DomainException("Email is invalid");

            if (dob > DateTime.UtcNow)
                throw new DomainException("Date of birth is invalid.");

            if (joinDate < dob)
                throw new DomainException("Join date cannot be before date of birth");

            if (!Enum.IsDefined(typeof(Gender), gender))
                throw new DomainException("Invalid gender");


        }

        private void SetFullName(string fullname)
        {
            if (string.IsNullOrWhiteSpace(fullname)) throw new DomainException("FullName is not empty!");
             FullName=fullname.Trim();
            Touch(UpdatedBy);


        }

        private void Touch(Guid userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }
    }


}

