namespace ManageVoyage.Models;

/// <summary>
/// Caro cuốc
/// </summary>
public class CaroBooking
{
    public int Id { get; set; }
    /// <summary>
    /// Ngày tạo cuốc
    /// </summary>
    public decimal? CreatedAt { get; set; }
    /// <summary>
    /// Điểm đón
    /// </summary>
    public string? SourceAddress { get; set; }
    /// <summary>
    /// Điểm trả
    /// </summary>
    public string? ReturnAddress { get; set; }
    /// <summary>
    /// Kinh độ
    /// </summary>
    public double? SourceLat { get; set; }
    /// <summary>
    /// Vĩ độ
    /// </summary>
    public double? SourceLong { get; set; }
    /// <summary>
    /// Trạng thái cuốc
    /// </summary>
    public string? Status { get; set; }
    /// <summary>
    /// Nguồn cuốc từ app nào
    /// </summary>
    public string? SourceApp { get; set; }
    /// <summary>
    /// Tên hãng hợp tác 
    /// </summary>
    public string? Partner { get; set; }
    /// <summary>
    /// Loại xe
    /// </summary>
    public string? CarType { get; set; }
    /// <summary>
    /// Tổng tiền thanh toán
    /// </summary>
    public decimal? TotalCash { get; set; }
    /// <summary>
    /// SDT Khach hàng
    /// </summary>
    public string? PhoneCustomer { get; set; }
    /// <summary>
    /// Tên khách hàng
    /// </summary>
    public string? NameCustomer { get; set; }
}
