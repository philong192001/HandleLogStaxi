using ManageVoyage.Common;
using ManageVoyage.Data;
using ManageVoyage.DTOs;
using ManageVoyage.Models;
using ManageVoyage.Repositories;
using ManageVoyage.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ManageVoyage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManageAppController : ControllerBase
{
    private readonly ICaroBookRepository _caroBookRepository;
    private readonly AppSetting _appSetting;
    private readonly IHttpClientFactory _factory;
    private readonly CaroBookingContext _caroBookingContext;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ManageAppController(CaroBookingContext caroBookingContext, IHttpClientFactory factory, AppSetting appSetting, ICaroBookRepository caroBookRepository)
    {
        _caroBookRepository = caroBookRepository;
        _appSetting = appSetting;
        _factory = factory;
        _caroBookingContext = caroBookingContext;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    /// <summary>
    /// API Update trạng thái đã cập nhật app hay chưa của khách
    /// </summary>
    /// <remarks>
    /// Here is an example of how to make a purchase request:
    ///
    ///     {
    ///         "FullName": "Giàng A Sắn",
    ///         "Phone" : "+842437183106"
    ///     }
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="200">Trả về response thành công : Update thành công cho account nào </response>
    /// <response code="404">Đã cập nhật cho khách  </response>
    /// <response code="400">Log exception và response 400</response>
    [HttpPost("UpdateInfoApp")]
    [ProducesResponseType(typeof(UpdateAppRequest), 200)]
    [ProducesResponseType(400)]
    public IActionResult UpdateStatusApp(UpdateAppRequest request)
    {
        try
        {
            var res = new ResponseAppDTO<string>();
            var record = _caroBookingContext.CaroBookingProcesses.Where(x => x.PhoneCustomer.Contains(request.Phone)).FirstOrDefault();

            if (record.FullName != null && record.InstallAppDate != null)
            {
                res.ErrorCode = ErrorCodeEnum.Reject;
                res.Data = $"Khách {record.FullName} đã cài app vào ngày {record.InstallAppDate}";
                res.Message = ErrorCodeEnum.Reject.GetDescription();
                return NotFound(res);
            }

            if (record == null)
            {
                res.ErrorCode = ErrorCodeEnum.NullRequest;
                res.Data = $"SĐT {request.Phone} không tồn tại";
                res.Message = ErrorCodeEnum.NullRequest.GetDescription();
                return NotFound(res);
            }

            record.InstallAppDate = DateTime.Now;
            record.FullName = request.FullName;
            _caroBookingContext.CaroBookingProcesses.Attach(record);
            // Đánh dấu chỉ cập nhật các trường cần thay đổi
            _caroBookingContext.Entry(record).Property(e => e.InstallAppDate).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.FullName).IsModified = true;
            _caroBookingContext.SaveChanges();

            res.ErrorCode = ErrorCodeEnum.Success;
            res.Data = $"Update Thành Công SDT {record.PhoneCustomer}";
            res.Message = ErrorCodeEnum.Success.GetDescription();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAppDTO<string>() { Data = ex.Message, ErrorCode = ErrorCodeEnum.Error, Message = ErrorCodeEnum.Error.GetDescription() });
        }
    }

    /// <summary>
    /// API CSKH Update sau khi tư vấn
    /// </summary>
    /// <remarks>
    /// Here is an example of how to make a purchase request:
    ///
    ///     {
    ///         "Status": "Chuyển đổi thành công",
    ///         "Line": 1,
    ///         "Phone" : "+842437183106"
    ///         "UpdatedByUser" : "longpv2"
    ///         "Note" : "Đã call và vận động thành công"
    ///     }
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="200">Trả về response thành công : Update thành công cho account nào </response>
    /// <response code="400">Log exception và response 400</response>
    [HttpPost("CSKHUpdate")]
    [ProducesResponseType(typeof(CSKHUpdateRequest), 200)]
    [ProducesResponseType(400)]
    public IActionResult CskhUpdate(CSKHUpdateRequest request)
    {
        try
        {
            var res = new ResponseAppDTO<string>();
            var record = _caroBookingContext.CaroBookingProcesses.Where(x => x.PhoneCustomer.Contains(request.Phone)).FirstOrDefault();
            if (record == null)
            {
                res.ErrorCode = ErrorCodeEnum.NullRequest;
                res.Data = $"SĐT {request.Phone} không tồn tại";
                res.Message = ErrorCodeEnum.NullRequest.GetDescription();
                return NotFound(res);
            }
            record.Status = request.Status;
            record.Line = request.Line;
            record.UpdatedByUser = request.UpdatedByUser;
            record.UpdatedDate = DateTime.Now;
            record.Note = request.Note;
            _caroBookingContext.CaroBookingProcesses.Attach(record);
            // Đánh dấu chỉ cập nhật các trường cần thay đổi
            _caroBookingContext.Entry(record).Property(e => e.Status).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.Line).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.UpdatedByUser).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.UpdatedDate).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.Note).IsModified = true;
            _caroBookingContext.SaveChanges();

            res.ErrorCode = ErrorCodeEnum.Success;
            res.Data = $"Update Thành Công Nội Dung CSKH cho khách {record.NameCustomer}";
            res.Message = ErrorCodeEnum.Success.GetDescription();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAppDTO<string>()
            {
                Data = ex.Message,
                ErrorCode = ErrorCodeEnum.Error,
                Message = ErrorCodeEnum.Error.GetDescription()
            });
        }
    }

    /// <summary>
    /// API Lấy lại data cuốc từ những ngày cũ (from - to cách tối đa 2 ngày)
    /// </summary>
    /// <remarks>
    /// Here is an example of how to make a purchase request:
    ///
    ///     {
    ///         "from": 1700326800,
    ///         "to": 1700499600
    ///     }
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="200">Trả về response thành công : Đã get được bao nhiêu record? Lưu được bao nhiêu xuống DB</response>
    /// <response code="400">Log exception và response 400</response>
    /// <response code="401">Thiếu Token</response>
    [HttpPost("UpdateByDate")]
    [ProducesResponseType(typeof(UpdateDateRequest), 200)]
    [ProducesResponseType(400)]
    public IActionResult UpdateByDate(UpdateDateRequest request)
    {
        try
        {
            var res = new ResponseAppDTO<string>();
            HttpClient client = _factory.CreateClient();
            var token = Utils.GetToken(client, _appSetting, _jsonSerializerOptions);

            var url = $"{_appSetting.CaroAccount.Url}{string.Format(Constants.UrlGetVoyage, 20000, request.To, request.From)}";
            // Thêm thông tin xác thực vào header "Authorization"
            client.DefaultRequestHeaders.Add("Authorization", token);
            //response từ API
            var response = client.GetAsync(url).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<VoyageCaroRes>(jsonData, _jsonSerializerOptions);

            foreach (var item in data.Data)
            {
                item.Customer.Phone = item.Customer.Phone.Replace("+84", "0");
            }

            if (data == null || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                res.ErrorCode = ErrorCodeEnum.Reject;
                res.Data = "No Token";
                res.Message = ErrorCodeEnum.Reject.GetDescription();
                return Unauthorized(res);
            }
            var result = _caroBookRepository.SaveRange(data);
            string[] parts = result.Split(':');

            res.ErrorCode = ErrorCodeEnum.Success;
            res.Data = $"Get Total : {data.Total} Record  - Save CaroBookings : {parts[0]} record  -- Save CaroBookingProcess : {parts[1]} record ";
            res.Message = ErrorCodeEnum.Success.GetDescription();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAppDTO<string>()
            {
                Data = ex.Message,
                ErrorCode = ErrorCodeEnum.Error,
                Message = ErrorCodeEnum.Error.GetDescription()
            });
        }
    }
}