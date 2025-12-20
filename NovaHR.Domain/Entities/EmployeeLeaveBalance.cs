using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;

namespace NovaHR.Domain.Entities
{
    public class EmployeeLeaveBalance : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id từ BaseEntity

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public int Year { get; private set; }
        public decimal RemainingDays { get; private set; }

        // -------------------------
        // 3. Quy tắc 3: Quan hệ (FK / Navigation)
        // -------------------------
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;

        public Guid LeaveTypeId { get; private set; }
        public LeaveType LeaveType { get; private set; } = null!;

        // -------------------------
        // 4. Quy tắc 4: Phục vụ hành vi / nghiệp vụ
        // -------------------------
        // Hành vi trừ ngày phép khi duyệt đơn nghỉ
        public void DeductDays(decimal days, Guid updatedBy)
        {
            if (days <= 0) throw new DomainException("Số ngày trừ phải > 0");
            if (RemainingDays < days) throw new DomainException("Không đủ ngày phép");

            RemainingDays -= days;
            Touch(updatedBy);
        }

        // -------------------------
        // 5. Quy tắc 5: Thao tác hệ thống (Audit + Soft Delete)
        // -------------------------
        
        private EmployeeLeaveBalance() { }

        public EmployeeLeaveBalance(Guid employeeId, Guid leaveTypeId, int year, decimal initialDays, Guid createdBy)
        {
            if (employeeId == Guid.Empty) throw new DomainException("EmployeeId bắt buộc");
            if (leaveTypeId == Guid.Empty) throw new DomainException("LeaveTypeId bắt buộc");
            if (initialDays < 0) throw new DomainException("Số ngày không hợp lệ");

            EmployeeId = employeeId;
            LeaveTypeId = leaveTypeId;
            Year = year;
            RemainingDays = initialDays;

            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy == Guid.Empty ? throw new DomainException("CreatedBy bắt buộc") : createdBy;
        }

       
    }
}