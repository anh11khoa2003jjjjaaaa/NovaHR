using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;

namespace NovaHR.Domain.Entities
{
    public class Shift : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id from BaseEntity

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string Name { get; private set; } = null!;
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; } 
        public int BreakMinutes { get; private set; }
        
        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        public bool IsActive { get; private set; } = true;

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
         

        // For EF Core
        protected Shift() { }

        public Shift(string name, TimeSpan start, TimeSpan end, int breakMinutes)
        {
            SetName(name);
            SetTime(start, end, breakMinutes);

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        // Rule 5: Behaviors
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Tên ca làm việc không được để trống");
            Name = name.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetTime(TimeSpan start, TimeSpan end, int breakMinutes)
        {
            if (end <= start)
                throw new DomainException("Giờ kết thúc phải sau giờ bắt đầu");

            if (breakMinutes < 0)
                throw new DomainException("Thời gian nghỉ không hợp lệ");

            StartTime = start;
            EndTime = end;
            BreakMinutes = breakMinutes;

            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
