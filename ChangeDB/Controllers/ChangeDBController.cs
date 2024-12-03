using ChangeDB.Contexts;
using ChangeDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChangeDB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChangeDBController : ControllerBase
{
    private readonly FloorG783DbContext _dbContext83;
    private readonly FloorG726DbContext _dbContext26;

    public ChangeDBController(FloorG783DbContext dbContext83, FloorG726DbContext dbContext26)
    {
        _dbContext83 = dbContext83;
        _dbContext26 = dbContext26;
    }

    [HttpGet]
    public IActionResult ChangeDB()
    {
        int batchSize = 5000; // Kích thước lô (số bản ghi mỗi lần)
        //int totalRecords = _dbContext83.FloorEmployeeHistoryResults.Where(x => x.CompanyId == 212).Count(); // Số lượng bản ghi tổng
        int totalRecords = _dbContext83.FloorEmployeeHistoryResults.Count(); // Số lượng bản ghi tổng
        int totalInserted = 0; // Tổng số bản ghi đã chèn

        // Lấy tất cả BookId đã tồn tại trong _dbContext26
        var existingBookIds = _dbContext26.FloorEmployeeHistoryResults
            //.Where(x => x.CompanyId == 212)
            .Select(x =>  x.Id )
            .ToHashSet();

        for (int offset = 0; offset < totalRecords; offset += batchSize)
        {
            // Lấy một lô bản ghi từ adminTrip83
            var adminTrip83Batch = _dbContext83.FloorEmployeeHistoryResults
                //.Where(x => x.CompanyId == 212)
                .OrderBy(x => x.Id) // Thứ tự sắp xếp để tránh trùng lặp trong các lô
                .Skip(offset)
                .Take(batchSize)
                .ToList();

            // Tạo danh sách chứa các bản ghi khác biệt để chèn
            var adminTripToInsert = adminTrip83Batch
                .Where(trip => !existingBookIds.Contains(trip.Id)) // So sánh cả hai trường
                .Select(record => new FloorEmployeeHistoryResult
                {
                    HistoryId = record.HistoryId,
                    VehiclePlate = record.VehiclePlate,
                    PrivateCode = record.PrivateCode,
                    DriverCode = record.DriverCode,
                    Fullname = record.Fullname,
                    Mobile = record.Mobile,
                    Confirm = record.Confirm,
                    VehicleSeatType = record.VehicleSeatType
                })
                .ToList();

            // Thêm các phần tử khác biệt vào DbContext thứ hai (_dbContext26)
            if (adminTripToInsert.Any())
            {
                _dbContext26.ChangeTracker.Clear();
                _dbContext26.FloorEmployeeHistoryResults.AddRange(adminTripToInsert);
                _dbContext26.SaveChanges();

                // Cộng dồn số bản ghi đã chèn
                totalInserted += adminTripToInsert.Count;
            }
        }
        return Ok(new { message = "Data merged and inserted successfully.", insertedCount = totalInserted });
    }
}
