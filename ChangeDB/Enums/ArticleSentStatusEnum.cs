using System.ComponentModel;

namespace ChangeDB.Enums;

public enum ArticleSentStatusEnum
{
    [Description("Tin đã được tạo")]
    Created = 1,
    [Description("Tin đã được gửi")]
    Sent = 2,
    [Description("Tin chưa được gửi")]
    NotSent = 3,
    [Description("Tin gửi thất bại")]
    Failed = 4,
    /// <summary>
    /// Phục vụ query api GetDriverInStatus - không lưu
    /// </summary>
    [Description("Tin đã đọc")]
    Read = 5
}
