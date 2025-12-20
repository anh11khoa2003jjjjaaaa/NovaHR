using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Enums
{
    public enum TokenStatus: byte
    {
        [EnumMember(Value = "Hoạt động")]
        Active=1,
        [EnumMember(Value = "Đã thu hồi")]
        Revoked=2,
        [EnumMember(Value = "Hết hạn")]
        Expired=3
    }
}
