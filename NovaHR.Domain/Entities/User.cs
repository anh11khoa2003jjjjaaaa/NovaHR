using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Entities
{
    public class User : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id
        public string Code { get; private set; } = null!;

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string FullName { get; private set; } = null!;
        public string? Email { get; private set; } = null!;
        public string? Phone { get; private set; }

        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public Guid RoleId { get; private set; }
        public ICollection<Account> Accounts { get; private set; } = new List<Account>();
        public Token? Token { get; private set; } // 1-1 Relationship

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        // ...

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
        

        protected User() { }

        public User(string fullName, string? email, Guid roleId, Guid createdBy)
        {
            SetFullName(fullName);
            Email = email?.Trim().ToLower(); // Basic normalization
            RoleId = roleId;

            Code = $"USR-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            IsDeleted = false;
        }

        // Behavior: Update Profile
        public void UpdateProfile(string fullName, string? phone, string? email, Guid updatedBy)
        {
            SetFullName(fullName);
            Phone = phone;
            if (!string.IsNullOrEmpty(email)) Email = email.Trim().ToLower();
            
            Touch(updatedBy);
        }

        public void ChangeRole(Guid newRoleId, Guid updatedBy)
        {
            if (newRoleId == Guid.Empty) throw new DomainException("Role ID không hợp lệ");
            RoleId = newRoleId;
            Touch(updatedBy);
        }

        private void SetFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Tên người dùng không được để trống");
            FullName = fullName.Trim();
        }

        
    }
}
