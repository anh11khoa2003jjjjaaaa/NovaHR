using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Enums
{
    public enum EmployeeStatus:byte
    {
        [EnumMember(Value = "Đang làm việc")]
        Active=1,
        [EnumMember(Value = "Nghỉ việc")]
        Inactive=2,
        [EnumMember(Value = "Chấm dứt hợp đồng")]
        Terminated=3


    }
}
