using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;

namespace NovaHR.Domain.Entities
{
    public class LeaveType : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id từ BaseEntity

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string Name { get; private set; } = null!; // Annual Leave, Sick Leave, Unpaid...
        public int DefaultDaysPerYear { get; private set; } // 12 ngày phép năm
        public bool IsPaid { get; private set; } // có lương hay không

        // -------------------------
        // 3. Quy tắc 3: Quan hệ (FK / Navigation)
        // -------------------------
        // Navigation ngược từ EmployeeLeaveBalance (không cần ICollection ở đây)

        // -------------------------
        // 4. Quy tắc 4: Phục vụ hành vi / nghiệp vụ
        // -------------------------
        // Không có hành vi phức tạp – chỉ là danh mục loại nghỉ

        // -------------------------
        // 5. Quy tắc 5: Thao tác hệ thống (Audit + Soft Delete)
        // -------------------------
         
        private LeaveType() { }

        public LeaveType(string name, int defaultDaysPerYear, bool isPaid, Guid createdBy)
        {
            Name = string.IsNullOrWhiteSpace(name)
                ? throw new DomainException("Tên loại nghỉ là bắt buộc")
                : name.Trim();

            if (defaultDaysPerYear < 0) throw new DomainException("Số ngày mặc định không hợp lệ");

            DefaultDaysPerYear = defaultDaysPerYear;
            IsPaid = isPaid;

            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy == Guid.Empty ? throw new DomainException("CreatedBy bắt buộc") : createdBy;
        }
    }
}