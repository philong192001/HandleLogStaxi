using System.ComponentModel;

namespace HandleLog.Models;

public class LogRequest
{
    //Date check folder theo ngày tháng năm dưới local - Từ ngày
    public string? FromDate { get; set; }

    //Date check folder theo ngày tháng năm dưới local - Đến ngày
    public string? ToDate { get; set; }

    //vehiclePlate check first prefix file log theo biển kiểm soát
    public string? VehiclePlate { get; set; }

    //Id Company để get configFTP and companyName , companyLogFolder
    public int CompanyId { get; set; }

    /// <summary>
    /// Loại log cần lấy
    /// 0 : Log CurrentApp
    /// 1 : Log Lái Xe
    /// </summary>
    public TypeLog TypeLog { get; set; }
}

public enum TypeLog
{
    /// <summary>
    /// Log App Sử dụng trên thiết bị
    /// </summary>
    [Description("Log CurrentApp")]
    CurrentApp = 0,

    /// <summary>
    /// Log Lái Xe
    /// </summary>
    [Description("Log Lái Xe")]
    Driver = 1
}