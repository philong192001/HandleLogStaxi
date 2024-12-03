using System.ComponentModel;

namespace ChangeDB.Enums;

public enum PaymentMethod : byte
{
    [Description("Tiền mặt")]
    ByMoney = 0,

    [Description("QR")]
    ByQR = 1,

    [Description("ViettelPay")]
    ViettelPay = 2,

    [Description("VNPAY đang xử lý")]
    VNPayProcessing = 3,

    [Description("Thanh toán ví KH")]
    Card = 4,

    /// <summary>
    /// thanh toán theo MoMo
    /// </summary>
    [Description("Momo")]
    MoMo = 5,

    /// <summary>
    /// trạng thái đang xử lý thanh toán momo
    /// </summary>
    [Description("Momo đang xử lý")]
    MoMoProcessing = 6,

    /// <summary>
    /// thanh toán chuyển khoản
    /// </summary>
    [Description("Chuyển khoản ngân hàng")]
    BankTransfer = 7,

    /// <summary>
    /// thanh toán appkh
    /// </summary>
    [Description("AppKH")]
    AppKh = 8,

    /// <summary>
    /// Thanh toán VNTaxi - trừ thẳng tk ngân hàng hoặc ví vnpay
    /// </summary>
    [Description("Ví VNPAY - Thanh toán Online")]
    VNTaxi = 9,

    /// <summary>
    /// Thanh toán VNTaxi - trừ thẳng tk ngân hàng hoặc ví vnpay
    /// </summary>
    [Description("Ví VNPAY - Đợi thanh toán")]
    VNTaxiProcessing = 10,

    /// <summary>
    /// Thanh toán VNTaxi đã thanh toán
    /// </summary>
    [Description("Ví VNPAY - Đã thanh toán")]
    VNTaxiPaid = 11,

    /// <summary>
    /// Thanh toán qua POS
    /// </summary>
    [Description("Thanh toán POS")]
    POS = 12,

    /// <summary>
    /// Thanh
    /// </summary>
    [Description("Thanh toán OnePay")]
    OnePay = 13,

    /// <summary>
    /// Đang xử lý thanh toán OnePay
    /// </summary>
    [Description("OnePay - Đợi thanh toán")]
    OnePayProcessing = 14,

    /// <summary>
    /// Thẻ Be
    /// </summary>
    [Description("Thẻ Be")]
    BeCard = 15,

    /// <summary>
    /// Trường hợp đang quét qrcode
    /// </summary>
    [Description("Quét qrcode đang quét")]
    InternalCardProcess = 18,

    /// <summary>
    /// Trạng thái đang thanh toán từ appkh
    /// </summary>
    [Description("AppKH đang thanh toán")]
    AppKhProcessing = 19,
    /// <summary>
    /// Khác
    /// </summary>
    [Description("Khác")]
    Another = 20,
    /// <summary>
    /// QRAppKH
    /// </summary>
    [Description("QRAppKH")]
    QRAppKH = 21,
}
