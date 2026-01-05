using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovaHR.Domain.Interfaces;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;

namespace NovaHR.Domain.Entities
{
    public class EmployeeSalary : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Rule 1: Inherited Id from BaseEntity
        public string Code { get; private set; } = null!;

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public SalaryType SalaryType { get; private set; } // [Standardized] Enum
        //Lương gốc
        public decimal FullBaseAmount { get; private set; }
        //Lương áp dụng để trả lương
        public decimal EffectiveBaseAmount => ProbationPercent.HasValue ? Math.Round(FullBaseAmount * ProbationPercent.Value / 100, 2) : FullBaseAmount;
        //public string Currency { get; private set; } = "VND";

        public DateTime EffectiveDate { get; private set; }         // Bắt đầu áp dụng
        public DateTime? EndDate { get; private set; }              // NULL = còn hiệu lực
        public decimal? ProbationPercent { get; private set; }      // 85% nếu đang thử việc

        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        // (Empty)

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------

        protected EmployeeSalary() { }

        public EmployeeSalary(Guid employeeId, decimal fullBaseAmount, SalaryType salaryType, DateTime effectiveDate, decimal? probationPercent)
        {


            EmployeeId = employeeId;
            SalaryType = salaryType;
            EffectiveDate = effectiveDate;
            FullBaseAmount = fullBaseAmount;
            Code = $"SAL-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            ProbationPercent = probationPercent > 0 ? probationPercent : null;
            EndDate = null;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        public void ApplyNewSalary(Employee employee, SalaryType salaryType, decimal fullAmountSalary, DateTime effectiveDate, decimal? probationPrecent = null)
        {
            var activeSalaries = employee.EmployeeSalaries.Where(e => e.EndDate == null).ToList();
            if (activeSalaries.Count > 1)
            {
                var errorMessage = $"DATA INTEGRITY VIOLATION: Employee {Id} has {activeSalaries.Count} active salary records. " +
                           $"Ids: {string.Join(", ", activeSalaries.Select(s => s.Id))}. " +
                           $"This should never happen. Immediate investigation required.";
                throw new InvalidOperationException(errorMessage);
            }
            var current = activeSalaries.FirstOrDefault();
            if (current != null)
            {
                current.Terminate(EffectiveDate);
            }


            var newSalary = new EmployeeSalary(employeeId: employee.Id, fullAmountSalary, salaryType: SalaryType.Monthly, effectiveDate, probationPrecent);
            employee.EmployeeSalaries.Add(newSalary);
        }
        // Behavior: Terminate Salary (End)
        public void Terminate(DateTime endDate)
        {
            if (endDate < EffectiveDate) throw new DomainException("Ngày kết thúc không thể trước ngày bắt đầu");
            EndDate = endDate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
