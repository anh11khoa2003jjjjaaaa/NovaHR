using System.Runtime.Serialization;

namespace NovaHR.Domain.Enums
{
    public enum AttendanceRecordStatus : byte
    {
        [EnumMember(Value = "Có mặt")]
        Present = 0,
        [EnumMember(Value = "Đúng giờ")]
        OnTime = 1,

        [EnumMember(Value = "Trễ giờ")]
        Late = 2,

        [EnumMember(Value = "Vắng mặt")]
        Absent = 3,

        [EnumMember(Value = "Đã duyệt")]
        Approved = 4
    }
}
