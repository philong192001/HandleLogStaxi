using ManageVoyage.Data;
using ManageVoyage.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManageVoyage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManageAppController : ControllerBase
{
    private readonly CaroBookingContext _caroBookingContext;

    public ManageAppController(CaroBookingContext caroBookingContext)
    {
        _caroBookingContext = caroBookingContext;
    }

    [HttpPost("UpdateInfoApp")]
    public IActionResult UpdateStatusApp(UpdateAppRequest request)
    {
        try
        {
            var record = _caroBookingContext.CaroBookingProcesses.Where(x => x.PhoneCustomer.Contains(request.Phone)).FirstOrDefault();
            record.InstallAppDate = request.InstallAppDate;
            record.FullName = request.FullName;
            _caroBookingContext.CaroBookingProcesses.Attach(record);
            // Đánh dấu chỉ cập nhật các trường cần thay đổi
            _caroBookingContext.Entry(record).Property(e => e.InstallAppDate).IsModified = true;
            _caroBookingContext.Entry(record).Property(e => e.FullName).IsModified = true;
            _caroBookingContext.SaveChanges();
            return Ok("Update Thành Công");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
