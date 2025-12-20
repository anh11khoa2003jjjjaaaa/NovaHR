using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;

namespace NovaHR.Domain.Entities
{
    public class Position : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string Name { get; private set; } = null!;
        public string Code { get; private set; } = null!;
        public string? Description { get; private set; }

        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        // (Empty)

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
         

        protected Position() { }

        public Position(string code, string name)
        {
            SetCode(code);
            SetName(name);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Tên chức vụ bắt buộc");
            Name = name.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new DomainException("Mã chức vụ bắt buộc");
            Code = code.Trim().ToUpper();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
