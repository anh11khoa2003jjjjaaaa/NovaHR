using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Enums
{
    public enum ProviderStatus:byte
    {

        [EnumMember(Value = "GOOGLE")]
        GOOGLE=1,
        [EnumMember(Value ="FACEBOOK")]
        FACEBOOK=2,
        [EnumMember(Value ="GITHUP")]
        GITHUP=3,
        [EnumMember(Value ="LOCAL")]
        LOCAL=4

    }
}
