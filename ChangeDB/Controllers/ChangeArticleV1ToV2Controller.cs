using ChangeDB.Contexts;
using ChangeDB.Contexts.MongoDb;
using ChangeDB.Models;
using ChangeDB.Models.v2;
using ChangeDB.Utils;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ChangeDB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChangeArticleV1ToV2Controller : ControllerBase
{
    private readonly IMongoDbContext _mongoDbContext;
    private readonly FloorG726DbContext _floorG726DbContext;
    private readonly IHttpClientFactory _factory;

    public ChangeArticleV1ToV2Controller(FloorG726DbContext floorG726DbContext, IHttpClientFactory factory, IMongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
        _floorG726DbContext = floorG726DbContext;
        _factory = factory;

    }

    [HttpGet("[action]")]
    public IActionResult CoppyArticleV1ToV2()
    {
        try
        {
            //var searchKeyword = "29E01681"; // Chuỗi bạn muốn tìm

            var compArticles = _floorG726DbContext.CompArticles.Where(x => x.IsDeleted != true).ToList();
            #region lọc ra các thông báo loại toàn công ty có SendType = 1
            //var articleSendByCompany = compArticles.Where(a => a.SendType == SendArticleType.SendByComp).ToList();

            // Truy vấn lấy PrivateCode và DriverCode từ cơ sở dữ liệu
            var drivers = _floorG726DbContext.Drivers
                .Select(a => new { a.PrivateCode, a.DriverCode, a.PK_DriverId })
                .ToList();
            var articleSendByUserAndGroup = compArticles.Where(a => a.SendType == SendArticleType.SendByDriver || a.SendType == SendArticleType.SendByGroup).ToList();
            //Console.WriteLine("ALL" + compArticles.Count);
            //Console.WriteLine("COMPANY" + articleSendByCompany.Count);
            //Console.WriteLine("LX + GROUP" + articleSendByUserAndGroup.Count);
            //lọc ra các thông báo theo loại lặp
            HttpClient client = _factory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(9);
            foreach (var article in articleSendByUserAndGroup)
            {
                // Tách chuỗi bằng dấu phẩy và thêm vào danh sách
                //List<int> driverIdList = new List<int>();
                List<int> driverIdList = string.IsNullOrEmpty(article.SendList)
                  ? new List<int>()
                  : article.SendList
                      .Split(',')
                      .Where(x => int.TryParse(x, out _))
                      .Select(int.Parse)
                      .ToList();

                // So sánh các giá trị
                var matchingDrivers = drivers
                    .Where(d => driverIdList.Contains(d.PK_DriverId))
                    .Select(x => x.DriverCode).ToList();
                // Chuyển đổi thành List<DayOfWeekEnum>
                List<DayOfWeekEnum> weekEnumList = new List<DayOfWeekEnum>();
                List<int> monthEnumList = new List<int>();
                string sendHour = "00:00:00";
                if (!string.IsNullOrEmpty(article.SendDayOfWeek))
                {
                    weekEnumList = article.SendDayOfWeek
                   .Split(',')
                   .Select(s => (DayOfWeekEnum)int.Parse(s.Trim()))
                   .ToList();
                }
                if (!string.IsNullOrEmpty(article.SendDayOfMonth))
                {
                    monthEnumList = article.SendDayOfMonth
                   .Split(',')
                   .Select(s => int.Parse(s.Trim()))
                   .ToList();
                }
                if (!string.IsNullOrEmpty(article.SendHour))
                {
                    // Chuyển đổi chuỗi thành định dạng giờ phút "HH:mm"
                    sendHour = TimeOnly.Parse(article.SendHour).ToString("HH:mm:ss");
                }


                var param = new ArticleV2()
                {
                    CompanyId = article.FK_CompanyID,
                    EmployeeId = article.CreatedByUser,
                    CreatedByUser = article.CreatedByUser,
                    Title = article.Title,
                    Content = article.Content,
                    ShortContent = article.ContentApp,
                    ParentArticleType = ParentArticleType.Common,
                    ChildArticleType = ChildArticleType.CompSend,
                    SendType = SendTypeArticleEnum.New,
                    SendGroupType = SendGroupType.SendByDriver, //lưu ý
                    SendList = matchingDrivers,
                    Period = article.Period == PeriodEnum.ByDay ? PeriodEnumV2.Day : article.Period == PeriodEnum.ByWeek ? PeriodEnumV2.Week : article.Period == PeriodEnum.ByMonth ? PeriodEnumV2.Month : PeriodEnumV2.None,
                    SendDayOfWeek = weekEnumList.Count > 0 ? weekEnumList : null,
                    SendDayOfMonth = monthEnumList.Count > 0 ? monthEnumList : null,
                    SendHour = sendHour,
                    StartDate = article.CreatedDate.CurrentSeconds(),
                    EndDate = article.CreatedDate.CurrentSeconds(),
                    CreatedDate = article.CreatedDate.CurrentSeconds(),
                    Status = ArticleStatusEnum.Done,
                };

                //if (param.SendList.Count() > 0)
                //{
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:1999/api/StaxiArticle/CreateArticle"),
                    Content = new StringContent(JsonSerializer.Serialize(param), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage response = client.SendAsync(request).Result;
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                //}
                //var a = JsonSerializer.Serialize(param);
            }
            #endregion

            //lọc ra các thông báo loại gửi theo nhóm hoặc từng lái xe
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Đọc file excel bắn noti firebase
    /// </summary>
    /// <returns></returns>
    [HttpPost("[action]")]
    public IActionResult RePushNoti([FromQuery] string folderPath)
    {
        try
        {
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                return BadRequest("Folder path is invalid or does not exist.");
            }
            var notificationResults = new List<string>();
            // Read all Excel files in the folder
            var excelFiles = Directory.GetFiles(folderPath, "*.xlsx")
                .Select(f => new FileInfo(f)) // Chuyển string thành FileInfo
                .OrderBy(f => f.LastWriteTime) // Sắp xếp theo LastWriteTime
                .ToList();

            foreach (var file in excelFiles)
            {
                try
                {
                    Console.WriteLine($"Processing file: {file.FullName}");
                    // Read Excel file
                    using var fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheetAt(0); // Read the first sheet

                    if (sheet != null)
                    {
                        for (int i = 1; i <= sheet.LastRowNum; i++) // Bắt đầu từ hàng 1 (hàng 0 là header)
                        {
                            var row = sheet.GetRow(i);
                            if (row == null) continue;
                            var evaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();

                            var vehiclePlate = row.GetCell(0);
                            var vehiclePlateValue = vehiclePlate?.ToString();
                            if (vehiclePlate != null && vehiclePlate.CellType == CellType.Formula)
                            {
                                // Đánh giá công thức và lấy giá trị thực
                                var evaluatedValue = evaluator.Evaluate(vehiclePlate);
                                vehiclePlateValue = evaluatedValue?.FormatAsString() ?? string.Empty;
                            }
                            Console.WriteLine($"vehiclePlateValue: {vehiclePlateValue}");

                            var driverId = _floorG726DbContext.DriverAssignCars.Where(x => x.VehiclePlate == vehiclePlateValue).Select(x => x.FK_DriverID).ToList();
                            var drivers = new List<Driver>(); //danh sách mã lái xe>
                            for (int j = 0; j < driverId.Count; j++)
                            {
                                var driverCode = _floorG726DbContext.Drivers.Where(x => x.PK_DriverId == driverId[j] && x.IsDeleted != true)?.Select(x => new Driver() { DeviceToken = x.DeviceToken, DriverCode = x.DriverCode }).FirstOrDefault();
                                if (driverCode != null)
                                {
                                    drivers.Add(driverCode);
                                }
                            }
                            //Tìm ra thông báo lưu trong mongo
                            string collectionNameRepeat = "ArticleRepeatPrivate202411";
                            //theo vehiclePlate tìm ra driverCode
                            var title = row.GetCell(1);
                            var titleValue = title?.ToString();
                            if (title != null && title.CellType == CellType.Formula)
                            {
                                var evaluatedValue = evaluator.Evaluate(title);
                                titleValue = evaluatedValue?.FormatAsString() ?? string.Empty;
                            }
                            //Console.WriteLine($"titleValue: {titleValue}");

                            if (!string.IsNullOrEmpty(titleValue))
                            {
                                var content = row.GetCell(2);
                                var contentValue = content?.ToString();

                                if (content != null && content.CellType == CellType.Formula)
                                {
                                    // Đánh giá công thức và lấy giá trị thực
                                    var evaluatedValue = evaluator.Evaluate(content);
                                    contentValue = evaluatedValue?.FormatAsString() ?? string.Empty;
                                }
                                //Console.WriteLine($"contentValue: {contentValue}");

                                foreach (var item in drivers)
                                {
                                    Console.WriteLine(item.DriverCode);
                                    var articleRepeat = _mongoDbContext.GetCollection<ArticleRepeat>(collectionNameRepeat).Find(x => x.DriverCode == item.DriverCode && x.Title == titleValue).FirstOrDefault();
                                    if (articleRepeat != null)
                                    {
                                        var firebaseArticleDatas = new FirebaseArticleDatas()
                                        {
                                            LinkArticle = $"https://app.g7taxi.vn/Home/Article_v2?id={articleRepeat._id.ToString()}&createdDate={articleRepeat.CreatedDate}&parentArticleType={(int)articleRepeat.ParentArticleType}"
                                        };
                                       // FireBaseUtil.SendNotification(titleValue, contentValue, item.DeviceToken, firebaseArticleDatas);
                                        // Thỉnh thoảng gửi cho token mới, ví dụ như cứ 5 lần gửi thì gửi 1 lần
                                        if (new Random().Next(1, 30) == 1) // Thỉnh thoảng chọn một token để gửi (1 lần trên 5 lần)
                                        {
                                            var token = "ebgjkGa5QyOsaIgtmdSgiP:APA91bEuBQQ6ULjCfOkIkIWaceb1xZBPLymQTkIyrLuDaUu5TlNVBEF01zrALcfJUH05TCrvFHf6-uC4173bQwHpDhqz8kA6XxAbXz61YNdGF-w8-GYCXeQ";
                                            FireBaseUtil.SendNotification(titleValue, contentValue, token, firebaseArticleDatas);

                                            var tokenTonnt = "d7y7XqRXT_WLqSko_ckbsl:APA91bFarI9BqcEjQVTdpRYqancPviR7NyhpXYAFAlJgC_Ystdkh1VQY3VxNq-CZtWuugh7VapPDmxoZ26POY7q7qdh0sf9kuWeFtiAzEeiEwufYRwzhjmw";
                                            FireBaseUtil.SendNotification(titleValue, contentValue, token, firebaseArticleDatas);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    notificationResults.Add($"{file}: Error - {ex.Message}");
                }
            }
            return Ok();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
}
