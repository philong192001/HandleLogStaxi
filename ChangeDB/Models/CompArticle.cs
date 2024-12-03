using System.ComponentModel;

namespace ChangeDB.Models;

public class CompArticle
{
    public int? ArticleID { set; get; }

    public int? FK_CompanyID { set; get; }

    public string? Title { set; get; }

    public string? Content { set; get; }

    public DateTime CreatedDate { set; get; }

    public DateTime? UpdatedDate { set; get; }
    public Guid? CreatedByUser { set; get; }
    public Guid? UpdatedByUser { set; get; }


    public int? ArticleType { set; get; }


    public SendArticleType? SendType { set; get; }


    public string? SendList { set; get; }

    public PeriodEnum? Period { get; set; }


    public string? SendMonth { set; get; }
    public string? SendDayOfMonth { set; get; }
    public string? SendDayOfWeek { set; get; }
    public string? SendHour { set; get; }
    public bool? IsSentNoPeriod { set; get; }
    public bool? IsDeleted { set; get; }
    public string? ContentNotify { set; get; }
    public bool? IsActive { set; get; }
    public int? CloneOfArticleID { set; get; }
    public string? ContentApp { set; get; }

}

public enum SendArticleType : int
{
    SendByComp = 1,
    SendByGroup = 2,
    SendByDriver = 3
}

public enum PeriodEnum
{
    [Description("Không lặp")]
    None = 0,

    [Description("Mỗi ngày")]
    ByDay = 1,

    [Description("Mỗi tuần")]
    ByWeek = 2,

    [Description("Mỗi tháng")]
    ByMonth = 3,

    [Description("Mỗi năm")]
    ByYear = 4
}