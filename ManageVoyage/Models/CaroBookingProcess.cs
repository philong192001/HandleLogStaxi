using System.Text.Json.Serialization;

namespace ManageVoyage.Models;

/// <summary>
/// Entity update trạng thái KH
/// </summary>
public class CaroBookingProcess
{
    /// <summary>
    /// SDT khách hàng
    /// </summary>
    [JsonPropertyName("PhoneNumber")]
    public string PhoneCustomer { get; set; }

    /// <summary>
    /// Tên khách hàng
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NameCustomer { get; set; }

    /// <summary>
    /// Trạng thái tài khoản khách
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; set; }

    /// <summary>
    ///
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Line { get; set; }

    /// <summary>
    /// Cập nhật bởi người nào
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UpdatedByUser { get; set; }

    /// <summary>
    /// Ngày cập nhật
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Ghi chú thêm
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Note { get; set; }

    /// <summary>
    /// Ngày khách cài đặt app Staxi
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("CreatedDate")]
    public DateTime? InstallAppDate { get; set; }

    /// <summary>
    /// Tên đầy đủ
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FullName { get; set; }

    /// <summary>
    /// ovverride method
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        CaroBookingProcess other = (CaroBookingProcess)obj;

        return PhoneCustomer == other.PhoneCustomer
            && NameCustomer == other.NameCustomer
            && Status == other.Status
            && Line == other.Line
            && UpdatedByUser == other.UpdatedByUser
            && UpdatedDate == other.UpdatedDate
            && Note == other.Note
            && InstallAppDate == other.InstallAppDate
            && FullName == other.FullName;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (PhoneCustomer != null ? PhoneCustomer.GetHashCode() : 0);
            hash = hash * 23 + (NameCustomer != null ? NameCustomer.GetHashCode() : 0);
            hash = hash * 23 + (Status != null ? Status.GetHashCode() : 0);
            hash = hash * 23 + (Line != null ? Line.GetHashCode() : 0);
            hash = hash * 23 + (UpdatedByUser != null ? UpdatedByUser.GetHashCode() : 0);
            hash = hash * 23 + (UpdatedDate != null ? UpdatedDate.GetHashCode() : 0);
            hash = hash * 23 + (Note != null ? Note.GetHashCode() : 0);
            hash = hash * 23 + (InstallAppDate != null ? InstallAppDate.GetHashCode() : 0);
            hash = hash * 23 + (FullName != null ? FullName.GetHashCode() : 0);
            return hash;
        }
    }
}