namespace ChangeDB.Models;

public class EmployeeShift
{
    public int Id { get; set; }

    public int? ParentLandmarkId { get; set; }

    public string? ChildLandmarkId { get; set; }

    public Guid? EmployeeId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LogoutTime { get; set; }
}
