using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Entities
{
    public class Token: AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Rule 1: Identity (Inherited)
        
        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public DateTime IssuedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime RefreshExpiresAt { get; private set; } // Renamed from Presfesh
        
        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public Guid UserId { get; private set; }
        public User? User { get; private set; } = null;

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        public TokenStatus TokenStatus { get; private set; }

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
         

        public Token() { }

        public Token(DateTime issuedAt, DateTime expiresAt, DateTime refreshExpiresAt, Guid userId, Guid createdBy)
        {
            if (userId == Guid.Empty) throw new DomainException("UserId là bắt buộc");
            if (createdBy == Guid.Empty) throw new DomainException("CreatedBy là bắt buộc");
            
            IssuedAt = issuedAt;
            ExpiresAt = expiresAt;
            RefreshExpiresAt = refreshExpiresAt; // Fixed typo
            UserId = userId;
            TokenStatus = TokenStatus.Active;
            
            CreatedAt = DateTime.UtcNow;        
            CreatedBy = createdBy;
            IsDeleted = false;
        }

        public void Revoke()
        {
            TokenStatus = TokenStatus.Revoked;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkExpired()
        {
            TokenStatus = TokenStatus.Expired;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}