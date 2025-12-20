using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;

namespace NovaHR.Domain.Entities
{
    // Rename class Roles -> Role (Singular is standard for Entities)
    public class Role : AuditableEntity
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
        // Có thể thêm List<Permission> Permissions { get; private set; } sau này

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        // (Empty)

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
        

        protected Role() { }

        public Role(string code, string name)
        {
            SetCode(code);
            SetName(name);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        public void SetName(string name)
        {
             if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Tên Role bắt buộc");
             Name = name.Trim();
             UpdatedAt = DateTime.UtcNow;
        }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new DomainException("Mã Role bắt buộc");
            Code = code.Trim().ToUpper();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
