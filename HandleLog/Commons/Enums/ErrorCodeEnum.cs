using System.ComponentModel;

namespace HandleLog.Commons.Enums;

public enum ErrorCodeEnum
{
    [Description("Thành công")]
    Success = 0,
    /// <summary>
    /// Lỗi Driver
    /// </summary>
    [Description("Lỗi: gửi tham số null")]
    NullRequest = 1,

    [Description("Error")]
    Error = 2,
}
