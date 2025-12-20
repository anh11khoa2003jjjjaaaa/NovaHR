using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;

namespace NovaHR.Domain.Entities
{
    public class PayrollDetail : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id từ BaseEntity

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string RuleCode { get; private set; } = null!; // OVERTIME, LATE_PENALTY, KPI_BONUS...
        public string Description { get; private set; } = null!;
        public decimal Amount { get; private set; } // có thể âm (phạt)

        // -------------------------
        // 3. Quy tắc 3: Quan hệ (FK / Navigation)
        // -------------------------
        public Guid PayrollRecordId { get; private set; }
        public PayrollRecord PayrollRecord { get; private set; } = null!;

        // -------------------------
        // 4. Quy tắc 4: Phục vụ hành vi / nghiệp vụ
        // -------------------------
        // Không có hành vi riêng – chi tiết lương chỉ lưu kết quả từ PayrollRule (Strategy Pattern)

        // -------------------------
        // 5. Quy tắc 5: Thao tác hệ thống (Audit + Soft Delete)
        // -------------------------
         
        private PayrollDetail() { } // EF Core

        public PayrollDetail(Guid payrollRecordId, string ruleCode, string description, decimal amount, Guid createdBy)
        {
            if (payrollRecordId == Guid.Empty) throw new DomainException("PayrollRecordId bắt buộc");
            if (string.IsNullOrWhiteSpace(ruleCode)) throw new DomainException("RuleCode bắt buộc");

            PayrollRecordId = payrollRecordId;
            RuleCode = ruleCode.Trim().ToUpper();
            Description = string.IsNullOrWhiteSpace(description) ? ruleCode : description.Trim();
            Amount = amount;

            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy == Guid.Empty ? throw new DomainException("CreatedBy bắt buộc") : createdBy;
        }
    }
}