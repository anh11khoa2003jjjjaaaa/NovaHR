using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public decimal BaseAmount { get; private set; }
        public string Currency { get; private set; } = "VND";
        
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

        public EmployeeSalary(Guid employeeId, SalaryType salaryType, decimal amount, DateTime effectiveDate)
        {
            if (amount < 0) throw new DomainException("Lương không được âm");
            
            EmployeeId = employeeId;
            SalaryType = salaryType;
            BaseAmount = amount;
            EffectiveDate = effectiveDate;
            
            Code = $"SAL-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        // Behavior: Update Amount
        public void UpdateAmount(decimal newAmount)
        {
            if (newAmount < 0) throw new DomainException("Lương không được âm");
            BaseAmount = newAmount;
            UpdatedAt = DateTime.UtcNow;
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
