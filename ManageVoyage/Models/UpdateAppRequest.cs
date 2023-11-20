namespace ManageVoyage.Models;

public class UpdateAppRequest
{
    public DateTime InstallAppDate { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
}

public class CSKHUpdateRequest
{
    public string Phone { get; set; }
    public string Status { get; set; }
    public int Line { get; set; }
    public string UpdatedByUser { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Note { get; set; }
}