namespace ChangeDB.Models;

/// <summary>
/// Gán tài xế cho xe
/// </summary>
public class DriverAssignCar
{
    /// <summary>
    /// ID công ty
    /// </summary>
    public int FK_CompanyID { get; set; }
    /// <summary>
    /// Id lái xe
    /// </summary>
    public int FK_DriverID { get; set; }
    /// <summary>
    /// Id phương tiện ( xe)
    /// </summary>
    public int FK_VehicleID { get; set; }
    /// <summary>
    /// BKS
    /// </summary>
    public string VehiclePlate { get; set; }
    /// <summary>
    /// Mã đàm
    /// </summary>
    public string PrivateCode { get; set; }
    /// <summary>
    /// Ngày gán
    /// </summary>
    public DateTime AssignedDate { get; set; }
}
