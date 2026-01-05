using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;


namespace NovaHR.Domain.Entities
{
    public class AttendanceRecord : AuditableEntity
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
        //Ngày chấm công là WorkDate
        public DateTime WorkDate { get; private set; }
        public int LateMinutes { get; private set; }
        public bool IsLate => LateMinutes > 0;
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
        public decimal TotalHours
        {
            get
            {
                if (CheckOutTime == null)

                    throw new DomainException("Chưa check-out, không thể tính TotalHours");

                return Math.Round(WorkMinutes / 60.0m, 2);

            }
        }


        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------


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
            //Lấy thời gian kết thúc của ngày làm việc
            //DateTime shiftEnd = GetShiftEndDateTime(WorkDate.Date, shift);
            DateTime shiftStart = GetShiftStartDateTime(WorkDate.Date, shift);

            //Kiểm tra nếu như mà thời gian điểm danh<= kết thúc ca thì không có đi trễ. Đi trễ thì ngược lại

            if (CheckInTime <= shiftStart)
                LateMinutes = 0;
            else
                LateMinutes = (int)(CheckInTime - shiftStart).TotalMinutes;
        }
        public DateTime GetShiftEndDateTime(DateTime datework, Shift shift)
        {
            return shift.EndTime > shift.StartTime ? datework.Date + shift.EndTime : datework.AddDays(1) + shift.EndTime;
        }

        public DateTime GetShiftStartDateTime(DateTime workDate, Shift shift)
        {
            return workDate.Date + shift.StartTime;
        }

        private void CalculateEarlyLeaveMinutes(Shift shift)
        {
            DateTime shiftTime = WorkDate.Date + shift.EndTime;
            if (CheckOutTime == null) return;

            if (CheckOutTime >= shiftTime)
                EarlyLeaveMinutes = 0;
            else
                EarlyLeaveMinutes = (int)(shiftTime - CheckOutTime.Value).TotalMinutes;
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

        public decimal CalculateDayWorking()
        {

            if (CheckOutTime == null) throw new DomainException("Chưa tính số ngày làm việc được vì chưa checkout");
            decimal hour = WorkMinutes / 60.0m;// Lấy số giờ làm việc
            decimal HoursStandant = 8m;// Mốc thời gian hành chính 8 tiếng
            decimal days = hour / HoursStandant;// Tính số ngày làm việc
            return Math.Min(Math.Round(days, 2), 1m);// lấy giá trị nhỏ nhất và làm tròn 2 chữ số


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
