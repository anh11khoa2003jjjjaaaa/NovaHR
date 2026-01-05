using NovaHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Interfaces.Rules
{
    public class RuleKPIScore : IPayrollRule
    {
        public string RuleName => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public decimal Apply(Employee employee, IEnumerable<AttendanceRecord> records)
        {
            throw new NotImplementedException();
        }
    }
}
