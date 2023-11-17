namespace ManageVoyage.Models;

public class CaroBooking
{
    public int Id { get; set; }
    public decimal? CreatedAt { get; set; }
    public string? SourceAddress { get; set; }
    public float? SourceLat { get; set; }
    public float? SourceLong { get; set; }
    public string? Status { get; set; }
    public string? SourceApp { get; set; }
    public string? Partner { get; set; }
    public string? CarType { get; set; }
    public decimal? Payment { get; set; }
    public string? PhoneCustomer { get; set; }
    public string? NameCustomer { get; set; }
}
