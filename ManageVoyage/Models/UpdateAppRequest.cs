using System.Text.Json.Serialization;

namespace ManageVoyage.Models;

/// <summary>
/// Request Cập nhật ngày khách chuyển đổi sang App G7
/// </summary>
public class UpdateAppRequest
{
    [JsonIgnore]
    public DateTime InstallAppDate { get; set; }

    /// <summary>
    /// Họ tên đầy đủ của khách
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Số điện thoại của khách (bắt buộc có để query)
    /// </summary>
    public string Phone { get; set; }
}

/// <summary>
/// Request CSKH Update thông tin cho khách
/// </summary>
public class CSKHUpdateRequest
{
    /// <summary>
    /// SDT Khách hàng
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Trạng thái tài khoản của khách
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int Line { get; set; }

    /// <summary>
    /// Cập nhật bởi nhân viên CSKH nào
    /// </summary>
    public string UpdatedByUser { get; set; }

    [JsonIgnore]
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// Ghi chú thêm
    /// </summary>
    public string Note { get; set; }
}