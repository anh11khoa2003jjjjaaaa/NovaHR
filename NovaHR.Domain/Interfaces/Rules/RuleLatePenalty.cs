using NovaHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Interfaces.Rules
{
    public class RuleLatePenalty : IPayrollRule
    {
        public string RuleName => "LatePenalty";

        public string Description => "Phạt đi trễ";

        public decimal Apply(Employee employee, IEnumerable<AttendanceRecord> records)
        {
            // Số tiền bị trừ
            decimal TotalMoneyGoLate = 0.0m;
            //Lặp qua số ngày nghỉ
            foreach (var record in records.Where(a => a.IsLate))
            {
                int lateMinute = record.LateMinutes;//Lấy số phút trễ
                decimal DailySalary = employee.CurrentBaseSalary / 176m;//Lấy số tiền một ngày/tháng
                decimal penalty = (lateMinute / 480) * DailySalary;//Tính tiền phạt
                TotalMoneyGoLate += penalty;// cộng

            }

            return TotalMoneyGoLate;



        }
    }
}
