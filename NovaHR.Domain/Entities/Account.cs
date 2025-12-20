using NovaHR.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;

namespace NovaHR.Domain.Entities
{
    public class Account: BaseEntity, IAuditableEntity, ISoftDelete
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id from BaseEntity
        public string Code { get; private set; } = null!;

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity (State)
        // -------------------------
        public string UserName { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public ProviderStatus Provider { get; private set; }

        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public Guid UserId { get; private set; }
        // public User User { get; private set; } // Lazy loading if needed

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        public AccountStatus AccountStatus { get; private set; }
   

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

        private Account() { }

        public Account(string username, string passwordHash, Guid userId, Guid createdBy)
        {
            UserName = string.IsNullOrWhiteSpace(username)
                ? throw new DomainException("Username là bắt buộc")
                : username.Trim().ToLower();

            PasswordHash = string.IsNullOrWhiteSpace(passwordHash)
                ? throw new DomainException("PasswordHash không hợp lệ")
                : passwordHash;

            if (userId == Guid.Empty) throw new DomainException("UserId là bắt buộc");
            if (createdBy == Guid.Empty) throw new DomainException("CreatedBy là bắt buộc");

            Code = GenerateCodeAccount(createdAt: DateTime.UtcNow);
            Provider = ProviderStatus.LOCAL;
            UserId = userId;
            AccountStatus = AccountStatus.Active;
            
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            IsDeleted = false;
        }

        // =======================================================
        // Hành vi: Lock/Unlock
        // =======================================================
        public void LockAccount(Guid lockedBy)
        {
            if (AccountStatus == AccountStatus.Locked)
                throw new DomainException("Tài khoản đã bị khóa trước đó");

            AccountStatus = AccountStatus.Locked;
            Touch(lockedBy);
        }

        public void UnlockAccount(Guid unlockedBy)
        {
             if (AccountStatus == AccountStatus.Active)
                throw new DomainException("Tài khoản đang hoạt động bình thường");

            AccountStatus = AccountStatus.Active;
            Touch(unlockedBy);
        }

        public void UpdatePassword(string newPasswordHash, Guid updatedBy)
        {
            PasswordHash = string.IsNullOrWhiteSpace(newPasswordHash)
                ? throw new DomainException("Password mới không hợp lệ")
                : newPasswordHash;

            Touch(updatedBy);
        }

        public void Deactivate(Guid deactivatedBy)
        {
            AccountStatus = AccountStatus.Inactive;
            Touch(deactivatedBy);
        }

        public void SoftDelete(Guid deletedBy)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = deletedBy;
            Touch(deletedBy);
        }

        private void Touch(Guid userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }

        private string GenerateCodeAccount(DateTime createdAt)
        {
            string datePart = $"{createdAt.Day}{createdAt.Month}{createdAt.Year}";
            return $"{datePart}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }
    }
}