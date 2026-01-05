using NovaHR.Domain.Entities;
using NovaHR.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateWorkingDays(this DateOnly start, DateOnly end, IEnumerable<Holiday>? holidays = null)
        {
            if (end < start)
                throw new DomainException("End date must be greater than or equal to start date");
            //Hàm này lấy số ngày lễ
            var holidayDates = holidays?.Select(h => h.HolidayDate).ToHashSet() ?? new HashSet<DateOnly>();
            int workingDays = 0;
            for (var date = start; date <= end; date.AddDays(1))
            {

                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !holidayDates.Contains(date))
                {
                    workingDays++;
                }
            }
            return workingDays;
            //Nếu như mà nghĩ lễ hoặc là thứ 7, chủ nhật thì không tính vào ngày làm việc

        }

    }

}

