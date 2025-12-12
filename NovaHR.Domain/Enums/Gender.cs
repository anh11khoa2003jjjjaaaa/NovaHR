using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Enums
{
    public enum Gender: byte
    {
        [EnumMember(Value ="Nam")]
        Male=1,
        [EnumMember(Value = "Nữ")]
        FaMale = 2,
        [EnumMember(Value = "Khác")]
        Other = 3
    }
}
