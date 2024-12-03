using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ManageVoyage.DTOs;

public class ResponseAppDTO<T>
{
    /// <summary>
    /// mã lỗi
    /// </summary>
    [JsonPropertyName("ErrorCode")]
    public ErrorCodeEnum ErrorCode { get; set; } = ErrorCodeEnum.Success;

    /// <summary>
    /// thông báo lỗi
    /// </summary>
    [JsonPropertyName("Message")]
    public string Message { get; set; } = ErrorCodeEnum.Success.GetDescription();

    /// <summary>
    /// dữ liệu trả về
    /// </summary>
    [JsonPropertyName("Data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }
}

public enum ErrorCodeEnum
{
    [Description("Thành công")]
    Success = 0,

    [Description("Lỗi: gửi tham số null")]
    NullRequest = 1,

    [Description("Error")]
    Error = 2,

    [Description("Từ chối dịch vụ")]
    Reject = 3,
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
}