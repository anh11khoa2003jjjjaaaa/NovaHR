using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;

namespace NovaHR.Domain.Entities
{
    public class AttendanceRecord : BaseEntity,IAuditableEntity, ISoftDelete
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Id inherited from BaseEntity
        
        public string Code { get; private set; } = null!;

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public DateTime CheckInTime { get; private set; }
        public DateTime? CheckOutTime { get; private set; }

        public int LateMinutes { get; private set; }
        public int EarlyLeaveMinutes { get; private set; }
        public int WorkMinutes { get; private set; }

        public AttendanceRecordStatus Status { get; private set; }

        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;

        public Guid ShiftId { get; private set; }
        public Shift Shift { get; private set; } = null!;

        public Guid? ApprovedBy { get; private set; }

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        // (Moved IsDeleted to Rule 5)

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        // =======================================================
        // Constructor bảo vệ (protected) cho ORM
        // =======================================================
        protected AttendanceRecord() { }

        // =======================================================
        // Factory method: Check-in (tạo record)
        // =======================================================
        public static AttendanceRecord CheckIn(
            Guid employeeId,
            Guid shiftId,
            DateTime checkInTime,
            Shift shift)
        {
            var record = new AttendanceRecord
            {
               
                EmployeeId = employeeId,
                ShiftId = shiftId,
                CheckInTime = checkInTime,
                Status = AttendanceRecordStatus.Present,
                Code = GenerateCodeStatic(),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            record.CalculateLateMinutes(shift);

            return record;
        }

        // =======================================================
        // Hành vi: Check-out (cập nhật record)
        // =======================================================
        public void CheckOut(DateTime checkout)
        {
            if (CheckOutTime != null)
                throw new DomainException("Nhân viên đã check-out trước đó.");

            if (checkout < CheckInTime)
                throw new DomainException("Check-out không thể trước check-in.");

            CheckOutTime = checkout;
            CalculateEarlyLeaveMinutes(Shift);
            CalculateWorkMinutes(Shift);

            UpdatedAt = DateTime.UtcNow;
        }

        // =======================================================
        // Hành vi: Duyệt bản ghi
        // =======================================================
        public void Approve(Guid approverId)
        {
            if (ApprovedBy != null)
                throw new DomainException("Bản ghi đã được duyệt rồi.");

            ApprovedBy = approverId;
            Status = AttendanceRecordStatus.Approved;
        }

        // =======================================================
        // Validate các logic trong domain
        // =======================================================
        private void CalculateLateMinutes(Shift shift)
        {
            if (CheckInTime <= shift.StartTime)
                LateMinutes = 0;
            else
                LateMinutes = (int)(CheckInTime - shift.StartTime).TotalMinutes;
        }

        private void CalculateEarlyLeaveMinutes(Shift shift)
        {
            if (CheckOutTime == null) return;

            if (CheckOutTime >= shift.EndTime)
                EarlyLeaveMinutes = 0;
            else
                EarlyLeaveMinutes = (int)(shift.EndTime - CheckOutTime.Value).TotalMinutes;
        }

        private void CalculateWorkMinutes(Shift shift)
        {
            if (CheckOutTime == null)
                throw new DomainException("Chưa thể tính giờ làm việc khi chưa check-out.");

            WorkMinutes = (int)(CheckOutTime.Value - CheckInTime).TotalMinutes
                          - LateMinutes
                          - EarlyLeaveMinutes
                          - shift.BreakMinutes;
        }

        // =======================================================
        // Code Generator
        // =======================================================
        private static string GenerateCodeStatic()
        {
            return $"ATT-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}
