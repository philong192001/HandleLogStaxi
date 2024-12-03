namespace ChangeDB.Models.v2;

/// <summary>
/// Thông báo master - không lặp
/// Nếu là thông báo lặp thì sinh ra thêm ở ArticleRepeat
/// </summary>
public class ArticleMaster : BaseEntityMongo
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
    /// Loại tin ( Thông báo ; Notify ; ..)
    /// </summary>
    public SendTypeArticleEnum SendType { get; set; } = 0;

    /// <summary>
    /// Loại nhóm gửi (công ty ; nhóm)
    /// </summary>
    public SendGroupType? SendGroupType { get; set; }

    /// <summary>
    /// Chu kỳ lặp (ngày ; tuần tháng ; không lặp)
    /// </summary>
    public PeriodEnum Period { get; set; }

    /// <summary>
    /// Nhận thông báo theo nhóm; lái xe; biển số
    /// </summary>
    public List<string>? SendList { get; set; }

    /// <summary>
    /// Thời gian gửi thông báo
    /// </summary>
    public TimeOnly SendHour { get; set; }

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

    public ArticleMaster()
    {

    }
}
