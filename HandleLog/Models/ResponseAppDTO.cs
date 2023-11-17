using HandleLog.Commons.Enums;
using System.Text.Json.Serialization;

namespace HandleLog.Models;

public class ResponseAppDTO<T>
{/// <summary>
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
    public T? Data { get; set; }
}
