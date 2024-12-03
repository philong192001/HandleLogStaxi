using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace ChangeDB.Models;

public class ArticleV2
{
    /// <summary>
    /// Mã lái xe
    /// </summary>
    public string? DriverCode { get; set; }
    /// <summary>
    /// Tiêu đề
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Nội dung
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Nội dung thông báo app
    /// </summary>
    public string? ShortContent { get; set; } = null;

    /// <summary>
    /// Loại thông báo cha
    /// </summary>
    public ParentArticleType ParentArticleType { get; set; }

    /// <summary>
    /// Loại thông báo con
    /// </summary>
    public ChildArticleType? ChildArticleType { get; set; }

    /// <summary>
    /// Loại tin ( Thông báo , Notify , ..)
    /// </summary>
    public SendTypeArticleEnum SendType { get; set; } = 0;

    /// <summary>
    /// Loại nhóm gửi (công ty , nhóm)
    /// </summary>
    public SendGroupType? SendGroupType { get; set; }

    /// <summary>
    /// Chu kỳ lặp (ngày , tuần tháng , không lặp)
    /// </summary>
    public PeriodEnumV2 Period { get; set; }

    /// <summary>
    /// Nhận thông báo theo nhóm, lái xe, biển số
    /// </summary>
    public List<string>? SendList { get; set; }

    /// <summary>
    /// Thời gian gửi thông báo
    /// </summary>
    public string SendHour { get; set; }

    /// <summary>
    /// Ngày gửi trong tháng (1-31)
    /// </summary>
    public List<int>? SendDayOfMonth { get; set; }

    /// <summary>
    /// Ngày gửi trong tuần (đơn vị : Thứ)
    /// </summary>
    public List<DayOfWeekEnum>? SendDayOfWeek { get; set; }

    /// <summary>
    /// Thời gian bắt đầu gửi thông báo
    /// </summary>
    public double StartDate { get; set; }

    /// <summary>
    /// Thời gian kết thúc gửi thông báo
    /// </summary>
    public double EndDate { get; set; }

    /// <summary>
    /// Trạng thái thông báo
    /// </summary>
    public ArticleStatusEnum Status { get; set; }

    public Guid? CreatedByUser { get; set; }

    /// <summary>
    /// ID công ty
    /// </summary>
    public int? CompanyId { get; set; }

    /// <summary>
    /// ID nhân viên
    /// </summary>
    public Guid? EmployeeId { get; set; }

    /// <summary>
    /// CreatedDate
    /// </summary>
    public double CreatedDate { get; set; }
}

public enum ParentArticleType
{
    /// <summary>
    /// Không có giá trị
    /// </summary>
    [Description("Không có giá trị")]
    None = -1,
    /// <summary>
    /// Thông báo chung gồm : Chính sách , Tin Tức, Khuyến Mại, Biên Bản
    /// </summary>
    [Description("Chung")]
    Common = 0,
    /// <summary>
    /// Thông báo cá nhân : Tài chính , Hãng gửi cho lái xe
    /// </summary>
    [Description("Cá Nhân")]
    Private = 1,
    /// <summary>
    /// Thông báo chuyến, cuốc : Sảnh , Chuyến đi
    /// </summary>
    [Description("Chuyến")]
    Trip = 2,
    /// <summary>
    /// Thông báo ghim, quan trọng : thông báo ghim để LX đọc bất cứ lúc nào , không bị trôi khi có thông báo mới 
    /// </summary>
    [Description("Ghim")]
    Pin = 3
}

public enum ChildArticleType
{
    /// <summary>
    /// Thông báo Chính sách
    /// </summary>
    [Description("Chính sách")]
    Policy = 1,

    /// <summary>
    /// Thông báo Tin Tức
    /// </summary>
    [Description("Tin tức")]
    News = 2,

    /// <summary>
    /// Thông báo Khuyến Mại
    /// </summary>
    [Description("Khuyến Mại")]
    Promotions = 3,

    /// <summary>
    /// Thông báo Biên Bản
    /// </summary>
    [Description("Biên Bản")]
    Report = 4,

    /// <summary>
    /// Thông báo Tài chính
    /// </summary>
    [Description("Tài Chính")]
    Finnacial = 5,

    /// <summary>
    /// Thông báo hãng gửi
    /// </summary>
    [Description("Hãng gửi")]
    CompSend = 6,

    /// <summary>
    /// Thông báo Sảnh
    /// </summary>
    [Description("Sảnh")]
    Floor = 7,

    /// <summary>
    /// Thông báo Chuyến đi
    /// </summary>
    [Description("Chuyến đi")]
    TripDriver = 8
}

public enum SendTypeArticleEnum
{
    [Description("Enum_Notice")]
    Notice = 0,
    [Description("Enum_News")]
    New = 1,
    [Description("Enum_Help")]
    Help = 2,
    [Description("Thông báo Notify")]
    Notify = 3,
}

public enum SendGroupType : byte
{
    [Description("Gửi theo công ty")]
    SendByComp = 1,
    [Description("Gửi theo nhóm")]
    SendByGroup = 2,
    [Description("Gửi theo lái xe")]
    SendByDriver = 3
}

/// <summary>
/// Chu kì lặp
/// </summary>
public enum PeriodEnumV2
{
    [Description("Không có dữ liệu")]
    Null = -1,
    [Description("Không lặp")]
    None = 0,
    [Description("Mỗi ngày")]
    Day = 1,
    [Description("Mỗi Tuần")]
    Week = 7,
    [Description("Mỗi Tháng")]
    Month = 30
}

public enum DayOfWeekEnum
{
    [Description("Chủ nhật")]
    Sun = 1,

    [Description("Thứ 2")]
    Mon = 2,

    [Description("Thứ 3")]
    Tue = 3,

    [Description("Thứ 4")]
    Wed = 4,

    [Description("Thứ 5")]
    Thu = 5,

    [Description("Thứ 6")]
    Fri = 6,

    [Description("Thứ 7")]
    Sat = 7,
}

/// <summary>
/// Phục vụ cho tb master
/// </summary>
public enum ArticleStatusEnum
{
    [Description("Đang hoạt động")]
    Active = 1,
    [Description("Đã dừng")]
    Pause = 2,
    [Description("Hoàn thành")]
    Done = 3,
    [Description("Đang gửi")]
    InProgress = 4,
}
