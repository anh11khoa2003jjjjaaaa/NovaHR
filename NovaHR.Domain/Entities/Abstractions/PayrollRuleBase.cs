using NovaHR.Domain.Entities;

namespace NovaHR.Domain.Interfaces
{
    public abstract class PayrollRuleBase : IPayrollRule
    {
        public abstract string RuleName { get; }
        public abstract string Description { get; }
        public abstract decimal Apply(Employee employee, IEnumerable<AttendanceRecord> records);



        
    }
}