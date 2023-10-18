namespace HandleLog.Models;

public class Company
{
    public Company()
    {
    }

    public Company(int id, string? name, string? linkTest, string? note, DateTime? createdDate, DateTime? updatedDate, int? status, DateTime? lastUpdate, string? linkRelease, string? port, string? xnCode, int? parentCompanyID, int? companyId, string? province, string? createdByUser, string? updatedByUser, bool? isDeleted, string? companyName, string? businessName, int? businessStatus, string? operatorAccount, DateTime? appChargeDate, DateTime? mapChargeDate, string? regionId, decimal? servicePrice, decimal? mapPrice, string? serviceMailTo, string? serviceMailCC, bool? isMiniCompany, string? iPServerFTP, int portFTP, string? userNameFTP, string? passwordFTP, string? directoryFTP, string? directoryLog)
    {
        Id = id;
        Name = name;
        LinkTest = linkTest;
        Note = note;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        Status = status;
        LastUpdate = lastUpdate;
        LinkRelease = linkRelease;
        Port = port;
        XnCode = xnCode;
        ParentCompanyID = parentCompanyID;
        CompanyId = companyId;
        Province = province;
        CreatedByUser = createdByUser;
        UpdatedByUser = updatedByUser;
        IsDeleted = isDeleted;
        CompanyName = companyName;
        BusinessName = businessName;
        BusinessStatus = businessStatus;
        OperatorAccount = operatorAccount;
        AppChargeDate = appChargeDate;
        MapChargeDate = mapChargeDate;
        RegionId = regionId;
        ServicePrice = servicePrice;
        MapPrice = mapPrice;
        ServiceMailTo = serviceMailTo;
        ServiceMailCC = serviceMailCC;
        IsMiniCompany = isMiniCompany;
        IPServerFTP = iPServerFTP;
        PortFTP = portFTP;
        UserNameFTP = userNameFTP;
        PasswordFTP = passwordFTP;
        DirectoryFTP = directoryFTP;
        DirectoryLog = directoryLog;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LinkTest { get; set; }
    public string? Note { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? Status { get; set; }
    public DateTime? LastUpdate { get; set; }
    public string? LinkRelease { get; set; }
    public string? Port { get; set; }
    public string? XnCode { get; set; }
    public int? ParentCompanyID { get; set; }
    public int? CompanyId { get; set; }
    public string? Province { get; set; }

    public string? CreatedByUser { get; set; }

    public string? UpdatedByUser { get; set; }

    public bool? IsDeleted { get; set; }

    public string? CompanyName { get; set; }

    public string? BusinessName { get; set; }

    public int? BusinessStatus { get; set; }

    public string? OperatorAccount { get; set; }

    public DateTime? AppChargeDate { get; set; }

    public DateTime? MapChargeDate { get; set; }

    public string? RegionId { get; set; }

    public decimal? ServicePrice { get; set; }

    public decimal? MapPrice { get; set; }

    public string? ServiceMailTo { get; set; }

    public string? ServiceMailCC { get; set; }

    public bool? IsMiniCompany { get; set; }

    public string? IPServerFTP { get; set; }
    public int PortFTP { get; set; }
    public string? UserNameFTP { get; set; }
    public string? PasswordFTP { get; set; }
    public string? DirectoryFTP { get; set; }
    public string? DirectoryLog { get; set; }
}