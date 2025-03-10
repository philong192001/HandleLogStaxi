namespace HandleLog.Models;

public class LogRequest
{
    //Date check folder theo ngày tháng năm dưới local - Từ ngày
    public string? FromDate { get; set; }

    //Date check folder theo ngày tháng năm dưới local - Đến ngày
    public string? ToDate { get; set; }

    //vehicalPlate check first prefix file log theo biển kiểm soát
    public string? VehicalPlate { get; set; }

    //Id Company để get configFTP and companyName , companyLogFolder
    public int CompanyId { get; set; }
}