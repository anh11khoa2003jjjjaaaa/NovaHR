using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;
using NovaHR.Domain.Interfaces;


namespace NovaHR.Domain.Entities
{
    public class Department : AuditableEntity
    {
        // -------------------------
        // 1. Quy tắc 1: Danh tính (Identity)
        // -------------------------
        // Inherited Id from BaseEntity
        public string Code { get; private set; } = null!;

        // -------------------------
        // 2. Quy tắc 2: Mô tả bản chất Entity
        // -------------------------
        public string DepartmentName { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        // -------------------------
        // 3. Quy tắc 3: Quan hệ / Foreign Keys
        // -------------------------
        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        // -------------------------
        // 4. Quy tắc 4: Thuộc tính phục vụ hành vi / nghiệp vụ
        // -------------------------
        public DepartmentStatus Status { get; private set; } = DepartmentStatus.Active;

        // -------------------------
        // 5. Quy tắc 5: Thuộc tính hệ thống (Audit)
        // -------------------------
        

        // ===== EF Core Contructor =====
        protected Department() { }

        // ===== Main Constructor =====
        public Department(string code, string departmentName, string description, Guid createdBy)
        {
            SetCode(code);
            SetName(departmentName);
            Description = description?.Trim() ?? string.Empty;

            CreatedBy = createdBy;
            UpdatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        // =======================================================
        // Behavior Methods
        // =======================================================

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Department name cannot be empty");

            DepartmentName = name.Trim();
            Touch(UpdatedBy ?? Guid.Empty);
        }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Department code cannot be empty");

            if (code.Length > 4)
                throw new DomainException("Department code must be <= 4 characters");

            Code = code.Trim().ToUpper();
        }

        public void AddEmployee(Employee employee, Guid userId)
        {
            if (employee == null) throw new DomainException("Invalid employee");

            if (Employees.Any(e => e.Id == employee.Id))
                throw new DomainException("Employee already exists in this department");

            Employees.Add(employee);
            Touch(userId);
        }

        public void RemoveEmployee(Guid employeeId, Guid userId)
        {
            var emp = Employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null) throw new DomainException("Employee not found");

            Employees.Remove(emp);
            Touch(userId);
        }

        public void Activate(Guid userId)
        {
            Status = DepartmentStatus.Active;
            Touch(userId);
        }

        public void Deactivate(Guid userId)
        {
            Status = DepartmentStatus.Inactive;
            Touch(userId);
        }

        
    }
}
