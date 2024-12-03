namespace ChangeDB.Models;

public class Driver
{
    public int PK_DriverId { get; set; }
    public string? DriverCode { get; set; }
    public string? PrivateCode { get; set; }
    public bool? IsDeleted { get; set; }
    public string? DeviceToken { get; set; }
}
