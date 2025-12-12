using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Enums
{
    public enum DepartmentStatus:byte
    {
        [EnumMember(Value = "Đang hoạt động")]
        Active=1,
        [EnumMember(Value = "Ngừng hoạt động")]
        Inactive=2,
    }
}
