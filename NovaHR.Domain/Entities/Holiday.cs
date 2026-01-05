using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;


namespace NovaHR.Domain.Entities
{
    public class Holiday : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id từ BaseEntity (Guid tự gen)

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string Name { get; private set; } = null!; // Tết Nguyên Đán, Quốc Khánh...
        public DateOnly HolidayDate { get; private set; }
        public decimal SalaryMultiplier { get; private set; } = 2.0m; // mặc định x2, có thể x3

        // -------------------------
        // 3. Quy tắc 3: Quan hệ (FK / Navigation)
        // -------------------------
        // Không có navigation vì Holiday là danh mục độc lập

        // -------------------------
        // 4. Quy tắc 4: Phục vụ hành vi / nghiệp vụ
        // -------------------------
        // Không có hành vi phức tạp (chỉ là danh mục tham chiếu)
        // Hành vi tính lương ngày lễ sẽ nằm ở PayrollEngine sau này

        // -------------------------
        // 5. Quy tắc 5: Thao tác hệ thống (Audit + Soft Delete)
        // -------------------------
        

        private Holiday() { } // EF Core

        public Holiday(string name, DateOnly holidayDate, decimal multiplier, Guid createdBy)
        {
            Name = string.IsNullOrWhiteSpace(name)
                ? throw new DomainException("Tên ngày lễ là bắt buộc")
                : name.Trim();

            if (holidayDate.Year < 2000) throw new DomainException("Ngày lễ không hợp lệ");

            if (multiplier < 1.0m) throw new DomainException("Hệ số lương phải >= 1");

            HolidayDate = holidayDate;
            SalaryMultiplier = multiplier;

            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy == Guid.Empty ? throw new DomainException("CreatedBy bắt buộc") : createdBy;
        }

        public void UpdateMultiplier(decimal newMultiplier, Guid updatedBy)
        {
            if (newMultiplier < 1.0m) throw new DomainException("Hệ số lương phải >= 1");
            SalaryMultiplier = newMultiplier;
            Touch(updatedBy);
        }

        
    }
}