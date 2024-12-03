namespace ChangeDB.Models;

public class FloorEmployeeHistoryResult
{
    public int Id { get; set; }
    public Guid? HistoryId { set; get; }
    public string? VehiclePlate { set; get; }
    public string? PrivateCode { set; get; }
    public string? DriverCode { set; get; }
    public string? Fullname { set; get; }
    public string? Mobile { set; get; }
    public bool? Confirm { set; get; }
    public int? VehicleSeatType { set; get; }
}
