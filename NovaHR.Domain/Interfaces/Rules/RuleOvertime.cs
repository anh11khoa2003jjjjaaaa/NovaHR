using NovaHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Interfaces.Rules
{
    public  class RuleOvertime : IPayrollRule
    {
         
        public string RuleName => "Overtime";
        public string Description => "Làm tăng ca";


        //Trả tiền tăng ca, không trả tổng tiền lương
        public decimal Apply(Employee employee, IEnumerable<AttendanceRecord> records)
        {
            decimal adjustment = 0m;
            decimal hourlyRate = employee.BaseSalary / 176m;
            foreach (AttendanceRecord record in records)
            {
                if (record.CheckOutTime == null) continue; // chưa check-out

                decimal totalHours = record.TotalHours;
                if (record.TotalHours <= 8) continue;// Không OT bỏ qua và tiếp tục
                decimal overtimeHours = record.TotalHours - 8;
                decimal result = overtimeHours * hourlyRate * 1.5m;
                adjustment += result;

            }
            return adjustment;

        }
    }
}
