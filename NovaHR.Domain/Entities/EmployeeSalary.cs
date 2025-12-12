using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Entities
{
    public class EmployeeSalary
    {
        public Guid Id { get; private set; }
        public int Code { get; private set; }

        public string SalaryType { get; private set; } = null!;
        public decimal BaseAmount { get; private set; }
        public string Currency { get; private set; } = "VND";
        public DateTime EffectiveDate { get;private set; }         // Bắt đầu áp dụng
        public DateTime? EndDate { get; private set; }// NULL = còn hiệu lực
        public decimal? ProbationPercent { get; private set; }      // 85% nếu đang thử việc
        
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;

    }
}
