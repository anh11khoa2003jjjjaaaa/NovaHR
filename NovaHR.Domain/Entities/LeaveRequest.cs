using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;


namespace NovaHR.Domain.Entities
{
    public class LeaveRequest : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string Reason { get; private set; } = null!;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        
        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;

        public Guid? ApproverId { get; private set; }

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        public LeaveRequestStatus Status { get; private set; } = LeaveRequestStatus.Pending;

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
       

        protected LeaveRequest() { }

        public LeaveRequest(Guid employeeId, string reason, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new DomainException("Lý do nghỉ phép không được để trống");

            if (startDate < DateTime.UtcNow.Date)
                throw new DomainException("Ngày nghỉ không thể là quá khứ");

            if (endDate < startDate)
                throw new DomainException("Ngày kết thúc phải sau ngày bắt đầu");

            EmployeeId = employeeId;
            Reason = reason.Trim();
            StartDate = startDate;
            EndDate = endDate;
            Status = LeaveRequestStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        // =======================================================
        // Behavior Methods
        // =======================================================

        public void Approve(Guid approverId)
        {
            if (Status != LeaveRequestStatus.Pending)
                throw new DomainException("Chỉ có thể duyệt đơn đang chờ");

            Status = LeaveRequestStatus.Approved;
            ApproverId = approverId;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = approverId;
        }

        public void Reject(Guid approverId)
        {
            if (Status != LeaveRequestStatus.Pending)
                throw new DomainException("Chỉ có thể từ chối đơn đang chờ");

            Status = LeaveRequestStatus.Rejected;
            ApproverId = approverId;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = approverId;
        }

        public void Cancel()
        {
            if (Status == LeaveRequestStatus.Approved)
                throw new DomainException("Không thể hủy đơn đã được duyệt");

            Status = LeaveRequestStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
