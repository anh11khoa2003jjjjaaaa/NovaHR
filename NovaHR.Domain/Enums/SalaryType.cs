using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace NovaHR.Domain.Enums
{
    public enum SalaryType : byte
    {
        // Lương tháng
        [EnumMember(Value = "Lương theo tháng")]
        Monthly = 1,
        // Lương giờ
        [EnumMember(Value = "Lương theo giờ")]
        Hourly = 2,
        // Lương theo dự án
        [EnumMember(Value = "Lương theo dự án")]
        ProjectBased = 3
    }
}
