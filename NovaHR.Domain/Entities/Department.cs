using NovaHR.Domain.Enums;
using NovaHR.Domain.Exceptions;

namespace NovaHR.Domain.Entities
{
    public class Department
    {
        // ==== Rule 1: Identity ====
        public Guid Id { get; private set; }
        public string Code { get; private set; } = null!;
        public string DepartmentName { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        // ==== Rule 2: Aggregate relationships ====
        private readonly List<Employee> _employees = new();
        public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

        // ==== Rule 3: Business attributes ====
        public DepartmentStatus Status { get; private set; } = DepartmentStatus.Active;

        // ==== Rule 4: Audit fields ====
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; private set; }
        public Guid UpdatedBy { get; private set; }


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
        }

        // ===== Behavior Methods =====

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Department name cannot be empty");

            DepartmentName = name.Trim();
            Touch(UpdatedBy);
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

            // Rule: A department cannot contain duplicate employees
            if (_employees.Any(e => e.Id == employee.Id))
                throw new DomainException("Employee already exists in this department");

            _employees.Add(employee);
            Touch(userId);
        }

        public void RemoveEmployee(Guid employeeId, Guid userId)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null) throw new DomainException("Employee not found");

            _employees.Remove(emp);
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

        private void Touch(Guid userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }
    }
}
