using ChangeDB.Enums;

namespace ChangeDB.Models;

public class CustBooksLobby
{
    public Guid? BookId { set; get; }

    public DateTime? SentTime { set; get; }

    public DateTime? CatchedTime { set; get; }

    public double? FromLat { set; get; }

    public double? FromLng { set; get; }

    public string? FromAddress { set; get; }

    public string? FromName { set; get; }

    public double? ToLat { set; get; }

    public double? ToLng { set; get; }

    public string? ToAddress { set; get; }

    public string? ToName { set; get; }

    public double? CurrentLat { set; get; }

    public double? CurrentLng { set; get; }

    public string? Mobile { set; get; }

    public int? CountCar { set; get; }

    public int? CarType { set; get; }

    public string? CarTypes { set; get; }

    public int? BookType { set; get; }

    public int? Priority { set; get; }

    public bool? IsReceivedCall { set; get; }

    public string? Comment { set; get; }

    public string? SaleOffCode { set; get; }

    public string? VehiclesDeny { set; get; }

    public bool? SourceCancel { set; get; }

    public string? ReasonSourceCancel { set; get; }

    public int? CancelType { set; get; }

    public DateTime? SourceCancalTime { set; get; }

    public int? FK_CompanyId { set; get; }

    public int? SourceType { set; get; }

    public int? TotalDone { set; get; }

    public int? TotalCancel { set; get; }

    public int? TotalMissTrip { set; get; }

    public string? CompanyIdsFavorite { set; get; }

    public string? CompanyIdsDeny { set; get; }

    public double? Money { set; get; }

    public double? TotalKm { set; get; }

    public int? Offer { set; get; }

    public int? TotalNominate { set; get; }

    public bool? WrongCustomer { set; get; }

    public string? WrongVehicle { set; get; }

    public int? OperationId { set; get; }

    public int? FK_RouteID { set; get; }

    public int? FK_SignCarDetailId { set; get; }

    public Guid? BookParent { set; get; }

    public int? BookTripType { set; get; }

    public string? VehiclePlateNominate { set; get; }

    public Guid? ReturnTripId { set; get; }

    public int? MoneyTip { set; get; }

    public int? Money2 { set; get; }

    public DistanceBy? DistanceProvider { set; get; }

    public string? DriverCancelMsg { set; get; }

    public bool? CanRetrip { set; get; }

    public Guid? KeyOffer { set; get; }

    public int? ComLandmarkId { set; get; }

    public Guid? Employee { set; get; }

    public bool? OperatorConfirm { set; get; }

    public bool? IsDeleted { set; get; }

    public DateTime? DateDeleted { set; get; }

    public DateTime? InsertedDate { set; get; }

    public int? ValueSaleOffCode { set; get; }

    public int? SumOffer { set; get; }

    public int? SumNormiate { set; get; }

    public DateTime? LastestDate { set; get; }

    public DateTime? OperatorDispatching { set; get; }

    public DateTime? OperatorDispatched { set; get; }

    public string? VehicleOperatorDispatched { set; get; }

    public int? TimeWaitOperatorDispatching { set; get; }

    public double? Distance { set; get; }

    public double? WaitMoney { set; get; }

    public int? MoneyExtend { set; get; }

    public int? MoneyExtendByTime { set; get; }

    public double? MoneyExtendFromDriver { set; get; }

    public double? WaitTime { set; get; }

    public int? TotalAlertSlow { set; get; }

    public int? CompanyIdChosen { set; get; }

    public bool? CalFeeTrip { set; get; }

    public bool? CalLockAuto { set; get; }

    public double? TripPrice { set; get; }

    public int? CalcType { set; get; }

    public int? DoneState { set; get; }

    public double? ManualMoney { set; get; }

    public string? TripSegments { set; get; }

    public byte? CustomerType { set; get; }

    public string? VehiclePlateChosen { set; get; }

    public string? PrivateCodeChosen { set; get; }

    public string? DriverCodeChosen { set; get; }

    public string? DisplayNameChosen { set; get; }

    public string? PhoneNumberChosen { set; get; }

    ////201/11/2019, thêm bookId chung giữa các hệ thống của HPGO, ID này có mã hóa
    public int? PublicBookId { set; get; }

    public string? SourceCoordinate { set; get; }

    public Guid? NewBookId { set; get; }

    public string? VehicleDenies { set; get; }

    public string? CustName { set; get; }

    public string? CustEmail { set; get; }

    public string? CustsInfo { set; get; }

    public int? ContractMoney { set; get; }

    public int? ContractAutoID { set; get; }

    public string? DriverNote { set; get; }

    public bool? IsUseInvoice { set; get; }

    public string? EInvoiceInfo { set; get; }

    public bool? IsOPContractBook { set; get; }

    public string? CustTaxCode { set; get; }

    public string? CustAddress { set; get; }

    public string? CustBuyer { set; get; }

    public string? InvoiceEmail { set; get; }

    public bool? IsSchedule { set; get; }

    public double? OtherLat { set; get; }

    public double? OtherLng { set; get; }

    public string? OtherAddress { set; get; }

    public string? OtherName { set; get; }

    public int? GoodsId { set; get; }

    public string? GoodsName { set; get; }

    public bool? IsAssignCar { set; get; }

    public string? AccountAssignCar { set; get; }

    public double? DistanceAssignCar { set; get; }

    public bool? IsShipped { set; get; }

    public bool? IsOPFixPrice { set; get; }

    public int? OPFixPrice { set; get; }

    public bool? OperatorCancel { set; get; }

    public int? PaidDriverMoney { set; get; }

    public string? PartnerBookId { set; get; }

    public PaymentMethod? PaymentType { set; get; }

    /// <summary>
    /// Thông tin SDT người được đặt hộ
    /// </summary>

    public string? BookForPhoneNumber { set; get; }

    /// <summary>
    /// Thông tin TÊN người được đặt hộ
    /// </summary>

    public string? BookForName { set; get; }

    /// <summary>
    /// Vị trí Lat hộp đen
    /// </summary>

    public double? BBCatchedLat { set; get; }

    /// <summary>
    /// Vị trí Lng hộp đen
    /// </summary>

    public double? BBCatchedLng { set; get; }

    /// <summary>
    /// Khoảng cách từ hộp đen đến vị trí đặt cuốc của KH
    /// </summary>

    public double? BBCatchedDistance { set; get; }

    /// <summary>
    /// Danh sách công ty khuyến mại
    /// </summary>

    public string? PromoteCompanyIds { set; get; }

    /// <summary>
    /// Cần thanh toán cho hãng
    /// </summary>

    public bool? NeedPaySubCompany { set; get; }

    public bool? VNPayPaid { set; get; }

    /// <summary>
    /// Hóa đơn điện tử
    /// </summary>
    public string? InvoiceCode { set; get; }

    public int? SignCarLandmarkId { set; get; }
}