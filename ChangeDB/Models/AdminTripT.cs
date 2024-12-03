namespace ChangeDB.Models;

public class AdminTripT
{
    public int? DriverId { set; get; }


    public string? DriverCode { set; get; }


    public int? CompanyId { set; get; }


    public long? VehicleId { set; get; }


    public string? VehiclePlate { set; get; }


    public Guid BookId { set; get; }


    public decimal? Distance { set; get; }


    public bool? Send { set; get; }


    public DateTime? SendTime { set; get; }


    public bool? Chosen { set; get; }


    public int? DriverState { set; get; }


    public int? DriverConfirm { set; get; }


    public int? Reason { set; get; }


    public DateTime? ConfirmTime { set; get; }


    public int? OperationId { set; get; }


    public double? Money { set; get; }


    public double? Lat { set; get; }


    public double? Lng { set; get; }


    public bool? SendCatchedUser { set; get; }


    public DateTime? DateSendCatchedUser { set; get; }


    public double? DistanceCatchedUser { set; get; }


    public bool? SendInvite { set; get; }


    public DateTime? DateSendInvite { set; get; }


    public int? TotalInvite { set; get; }


    public int? ReasonState { get; set; }


    public bool? Timeout { set; get; }


    public DateTime? DateDriverCancel { set; get; }


    public string? DriverCancelMsg { set; get; }


    public DateTime? DateDriverDone { set; get; }


    public int? SourceType { set; get; }


    public bool? BlackBoxLostGSM { set; get; }


    public int? CancelType { set; get; }


    public double? TotalKm { set; get; }


    public int? Offer { set; get; }


    public Guid? ReturnTripId { set; get; }


    public Guid? KeyOffer { set; get; }


    public double? LatCatchedUser { set; get; }


    public double? LngCatchedUser { set; get; }


    public double? LatSendDone { set; get; }


    public double? LngSendDone { set; get; }


    public double? LatSendInvite { set; get; }


    public double? LngSendInvite { set; get; }


    public double? LatCancel { set; get; }


    public double? LngCancel { set; get; }


    public string? VehiclePrivate { set; get; }


    public int? SCarType { set; get; }


    public int? CancelOverTime { set; get; }


    public bool? ReceivedBook { set; get; }


    public string? FromAddress { set; get; }


    public bool? SourceCancel { set; get; }


    public DateTime? CatchedTime { set; get; }


    public int? ValueSaleOffCode { set; get; }


    public double? TripPrice { set; get; }


    public int? MoneyExtend { set; get; }


    public int? MoneyExtendByTime { set; get; }


    public double? MoneyExtendFromDriver { set; get; }


    public double? WaitMoney { set; get; }


    public double? ManualMoney { set; get; }


    public string? TripSegments { set; get; }


    public DateTime? SentTime { set; get; }


    public int? FK_CompanyId { set; get; }


    public int? ComLandmarkId { set; get; }


    public Guid? Employee { set; get; }


    public int? LotIndex { set; get; }


    public bool? IsVirtualRadio { set; get; }

    public int? TotalDone { set; get; }
    public int? TotalCancel { set; get; }
}
