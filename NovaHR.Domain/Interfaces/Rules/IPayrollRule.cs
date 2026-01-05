using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NovaHR.Domain.Entities;

namespace NovaHR.Domain.Interfaces
{
    public interface IPayrollRule
    {
        /// <summary>
        /// Tên định danh của quy tắc tính lương (VD: "Overtime Rule", "Late Penalty Rule")
        /// </summary>
        string RuleName { get; }
        string Description { get; }

        /// <summary>
        /// Tính toán số tiền (thưởng hoặc phạt) dựa trên nhân viên và dữ liệu chấm công.
        /// </summary>
        decimal Apply(Employee employee, IEnumerable<AttendanceRecord> records);
    }
}
