using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Enums;
using NovaHR.Domain.Interfaces; // Assumed existence

namespace NovaHR.Domain.Entities
{
    public class PayrollRecord : BaseEntity, IAuditableEntity, ISoftDelete
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public int Month { get; private set; }
        public int Year { get; private set; }

        public decimal BaseSalary { get; private set; }
        public decimal Allowance { get; private set; } // Phụ cấp
        public decimal Bonus { get; private set; }     // Thưởng
        public decimal Deduction { get; private set; } // Khấu trừ (thuế, phạt)
        public decimal NetSalary { get; private set; } // Thực nhận

        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        public PayrollStatus Status { get; private set; } = PayrollStatus.Draft; // [Enum Updated]
        public DateTime? PaidDate { get; private set; }

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        protected PayrollRecord() { }

        public PayrollRecord(Guid employeeId, int month, int year, decimal baseSalary)
        {
            if (month < 1 || month > 12) throw new DomainException("Tháng không hợp lệ");
            if (year < 2000) throw new DomainException("Năm không hợp lệ");

            EmployeeId = employeeId;
            Month = month;
            Year = year;
            BaseSalary = baseSalary;
            Status = PayrollStatus.Draft;
            
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;

            CalculateNetSalary();
        }

        // Logic tính lương cơ bản (Demo)
        public void AddEarnings(decimal allowance, decimal bonus)
        {
            Allowance += allowance;
            Bonus += bonus;
            CalculateNetSalary();
        }

        public void AddDeduction(decimal amount)
        {
            Deduction += amount;
            CalculateNetSalary();
        }

        public void MarkAsPaid()
        {
            Status = PayrollStatus.Paid;
            PaidDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        private void CalculateNetSalary()
        {
            NetSalary = BaseSalary + Allowance + Bonus - Deduction;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
