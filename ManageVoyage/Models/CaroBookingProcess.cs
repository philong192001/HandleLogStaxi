namespace ManageVoyage.Models;

public class CaroBookingProcess
{
    public int Id { get; set; }
    public string PhoneCustomer { get; set; }
    public string NameCustomer { get; set; }
    public string? Status { get; set; }
    public int? Line { get; set; }
    public string? UpdatedByUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? Note { get; set; }
    public DateTime? InstallAppDate { get; set; }
    public string? FullName { get; set; }
}
