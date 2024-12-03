namespace ChangeDB.Models;

public class FloorEmployeeHistory
{
    public Guid Id { get; set; }
    public int? ActionType { get; set; }
    public Guid EmployeeId { get; set; }
    public int? LandmarkId { get; set; }
    public string? LandmarkName { get; set; }
    public int? CountVehicle4 { get; set; }
    public int? CountVehicle7 { get; set; }
    public int? CountVehicleAll { get; set; }
    public DateTime CreatedDate { get; set; }
}
