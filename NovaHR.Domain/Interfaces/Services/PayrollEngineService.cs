using NovaHR.Domain.Entities;
using NovaHR.Domain.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Interfaces.Services
{
    public class PayrollEngineService
    {
        public readonly List<IPayrollRule> _rules;
        public PayrollEngineService()
        {
            _rules = new List<IPayrollRule>
            {
                new RuleOvertime(),
                new RuleLatePenalty(),
                new RuleKPIScore(),

            };
        }
        public decimal CalculatedNetSalary( Employee employee, IEnumerable<AttendanceRecord> attendanceRecord )
        {
            decimal net = employee.BaseSalary;
            foreach(var rule in _rules )
            {
                net += rule.Apply(employee, attendanceRecord);
            }
            return net;
        }
    }
}
