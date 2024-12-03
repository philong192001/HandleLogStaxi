using ManageVoyage.Data;
using ManageVoyage.DTOs;
using ManageVoyage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManageVoyage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingProcessController : ControllerBase
{
    private readonly CaroBookingContext _caroBookingContext;
    private readonly IHttpClientFactory _factory;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private const string API_G7_USER_USING_APP = "http://g7.staxi.vn:12608/api/GetG7UsedApp";

    public BookingProcessController(IHttpClientFactory factory, CaroBookingContext caroBookingContext)
    {
        _caroBookingContext = caroBookingContext;
        _factory = factory;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    [HttpGet]
    public IActionResult LoadData()
    {
        try
        {
            HttpClient client = _factory.CreateClient();
            var response = client.GetAsync(API_G7_USER_USING_APP).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<ResponseAppDTO<List<CaroBookingProcess>>>(jsonData, _jsonSerializerOptions);
            Dictionary<string, CaroBookingProcess> dicInsert = new Dictionary<string, CaroBookingProcess>();
            Dictionary<string, CaroBookingProcess> dicUpdate = new Dictionary<string, CaroBookingProcess>();
            var allCustomer = _caroBookingContext.CaroBookingProcesses.ToList();

            if (data.Data != null)
            {
                foreach (var item in data.Data)
                {
                    // Kiểm tra xem dưới DB có chưa, nếu có rồi thì update
                    var existingCustomer = allCustomer.FirstOrDefault(c => c.PhoneCustomer.Contains(item.PhoneCustomer));

                    if (existingCustomer != null)
                    {
                        // Kiểm tra xem trong dic có key này chưa
                        if (!dicUpdate.ContainsKey(existingCustomer.PhoneCustomer))
                        {
                            existingCustomer.FullName = item.FullName;
                            existingCustomer.Status = "Đã cài App G7";
                            existingCustomer.UpdatedByUser = "longpv2";
                            existingCustomer.UpdatedDate = DateTime.Now;
                            existingCustomer.InstallAppDate = item.InstallAppDate;
                            dicUpdate.Add(existingCustomer.PhoneCustomer, existingCustomer);
                        }
                    }
                    // Lọc trùng dữ liệu từ API trước khi insert vì PhoneCustomer là unique
                    else if (!dicInsert.ContainsKey(item.PhoneCustomer))
                    {
                        item.Status = "Đã cài App G7";
                        item.UpdatedByUser = "longpv2";
                        item.UpdatedDate = DateTime.Now;
                        dicInsert.Add(item.PhoneCustomer, item);
                    }
                }

                if (dicInsert.Count > 0)
                {
                    _caroBookingContext.AddRange(dicInsert.Values);
                }

                if (dicUpdate.Count > 0)
                {
                    _caroBookingContext.UpdateRange(dicUpdate.Values);
                }
                _caroBookingContext.SaveChanges();
            }
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}