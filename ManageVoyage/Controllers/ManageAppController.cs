using ManageVoyage.Common;
using ManageVoyage.Data;
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

    [HttpPost("UpdateInfoApp")]
    public IActionResult UpdateStatusApp(UpdateAppRequest request)
    {
        try
        {
            var record = _caroBookingContext.CaroBookingProcesses.Where(x => x.PhoneCustomer.Contains(request.Phone)).FirstOrDefault();

            if(record.FullName != null && record.InstallAppDate != null)
            {
                return NotFound($"Khách {record.FullName} đã cài app vào ngày {record.InstallAppDate}");
            }

            record.InstallAppDate = request.InstallAppDate;
            record.FullName = request.FullName;
            _caroBookingContext.CaroBookingProcesses.Attach(record);
            // Đánh dấu chỉ cập nhật các trường cần thay đổi
            _caroBookingContext.Entry(record).Property(e => e.InstallAppDate).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.FullName).IsModified = true;
            _caroBookingContext.SaveChanges();
            return Ok($"Update Thành Công SDT {record.PhoneCustomer}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("CSKHUpdate")]
    public IActionResult CskhUpdate(CSKHUpdateRequest request)
    {
        try
        {
            var record = _caroBookingContext.CaroBookingProcesses.Where(x => x.PhoneCustomer.Contains(request.Phone)).FirstOrDefault();   

            record.Status = request.Status;
            record.Line = request.Line;
            record.UpdatedByUser = request.UpdatedByUser;
            record.UpdatedDate = request.UpdatedDate;
            record.Note = request.Note;
            _caroBookingContext.CaroBookingProcesses.Attach(record);
            // Đánh dấu chỉ cập nhật các trường cần thay đổi
            _caroBookingContext.Entry(record).Property(e => e.Status).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.Line).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.UpdatedByUser).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.UpdatedDate).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.Note).IsModified = true;
            _caroBookingContext.SaveChanges();
            return Ok($"Update Thành Công Nội Dung CSKH cho khách {record.NameCustomer}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("UpdateToDate")]
    public IActionResult UpdateToDate(UpdateDateRequest request)
    {
        try
        {
            HttpClient client = _factory.CreateClient();
            var token = Utils.GetToken(client, _appSetting, _jsonSerializerOptions); 

            var url = $"{_appSetting.CaroAccount.Url}{string.Format(Constants.UrlGetVoyage, 20000, request.To, request.From)}";
            // Thêm thông tin xác thực vào header "Authorization"
            client.DefaultRequestHeaders.Add("Authorization", token);
            //response từ API
            var response = client.GetAsync(url).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<VoyageCaroRes>(jsonData, _jsonSerializerOptions);

            if (data == null || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized("NO TOKEN");
            }
            _caroBookRepository.SaveRange(data);

            return Ok($"SUCCESS Done Save {data.Total} Record" );
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }


 
}
