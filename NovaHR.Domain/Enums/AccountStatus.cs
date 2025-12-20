using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Enums
{
    public enum AccountStatus
    {
        [EnumMember(Value = "Hoạt động")]
        Active = 1,
        [EnumMember(Value = "Vô hiệu hóa")]
        Inactive = 2,
        [EnumMember(Value = "Đã khóa")]
        Locked = 3
            
    }
}
