using HandleLog.Commons.Enums;
using HandleLog.Commons.Utils;
using HandleLog.Contexts;
using HandleLog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;

#nullable disable

namespace HandleLog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoggingController : ControllerBase
{
    private readonly AppSetting _appSetting;
    private readonly LandingPageManagementDbContext _dbContext;

    public LoggingController(AppSetting appSetting, LandingPageManagementDbContext dbContext)
    {
        _appSetting = appSetting;
        _dbContext = dbContext;
    }

    [HttpPost("SendLogCustomer")]
    public IActionResult SendLogCustomer(LogRequest logRequest)
    {
        try
        {
            var response = new ResponseAppDTO<List<string>>();
            // Lấy cấu hình của công ty
            var getConfigFTP = _dbContext.Companies.Where(x => x.CompanyId.Equals(logRequest.CompanyId)).Select(x => new { x.IPServerFTP, x.PortFTP, x.UserNameFTP, x.PasswordFTP, x.DirectoryFTP, x.DirectoryLog }).FirstOrDefault();
            if (getConfigFTP == null)
            {
                return BadRequest(new ResponseAppDTO<string>
                {
                    Message = "Không tìm thấy cấu hình cho công ty",
                    ErrorCode = ErrorCodeEnum.Error
                });
            }
            // Lấy danh sách thư mục có chứa log
            string[] parentDirectories = Directory.GetDirectories(_appSetting.PathRoot, $"{getConfigFTP.DirectoryLog}*");
            if (parentDirectories.Length == 0)
            {
                return Ok(new ResponseAppDTO<List<string>> { Message = "Không có thư mục log tồn tại" });
            }

            // Chuyển đổi fromDate & toDate từ string sang DateTime để so sánh chính xác hơn
            if (!DateTime.TryParseExact(logRequest.FromDate, "dd_MM_yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate) ||
                !DateTime.TryParseExact(logRequest.ToDate, "dd_MM_yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate))
            {
                return BadRequest(new ResponseAppDTO<string>
                {
                    Message = "Định dạng fromDate hoặc toDate không hợp lệ. Định dạng đúng: dd_MM_yyyy",
                    ErrorCode = ErrorCodeEnum.Error
                });
            }
            var outputFilePaths = new List<string>();
            string vehiclePlateLower = logRequest.VehiclePlate.ToLower(); // chuẩn hóa biển số xe về chữ thường để so sánh

            // Duyệt qua từng thư mục cấp 1
            foreach (string parentDir in parentDirectories)
            {
                // Lấy danh sách thư mục con (theo ngày)
                string[] subDirectories = Directory.GetDirectories(parentDir);

                foreach (string subDir in subDirectories)
                {
                    string folderName = Path.GetFileName(subDir); // "14_03_2025"

                    // Kiểm tra nếu thư mục có dạng dd_MM_yyyy và nằm trong khoảng thời gian yêu cầu
                    if (!DateTime.TryParseExact(folderName, "dd_MM_yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime folderDate) ||
                        folderDate < fromDate || folderDate > toDate)
                    {
                        continue; // Bỏ qua nếu không hợp lệ
                    }

                    if (logRequest.TypeLog == TypeLog.Driver)
                    {
                        // Đường dẫn file log đã merge
                        string nameFileOutPut = $"TotalLog_{logRequest.VehiclePlate}{_appSetting.NameFileLogMerge}";
                        string outputFilePath = Path.Combine(subDir, nameFileOutPut).Replace("\\", "/");

                        // Merge tất cả file log theo biển số xe trong thư mục con
                        FileUtil.MergeFilesInFolder(subDir, outputFilePath, logRequest.VehiclePlate, _appSetting.AttrLog);

                        var directoryName = Directory.CreateDirectory(parentDir).Name;
                        if (System.IO.File.Exists(outputFilePath))
                        {
                            string fileUrl = $"{_appSetting.CurrentLink}{directoryName}/{folderName}/{nameFileOutPut}";
                            outputFilePaths.Add(fileUrl);
                        }
                    }

                    if (logRequest.TypeLog == TypeLog.CurrentApp)
                    {
                        // Định dạng tiền tố file log cần lấy
                        string prefix = $"{vehiclePlateLower}_";

                        // Lấy tất cả file trong thư mục con (subDir)
                        var logFiles = Directory.EnumerateFiles(subDir)
                                               .Where(file =>
                                               {
                                                   string fileNameLower = Path.GetFileName(file).ToLower();
                                                   return fileNameLower.StartsWith(prefix) && fileNameLower.Contains("currentapp");
                                               })
                                               .OrderByDescending(file => System.IO.File.GetLastWriteTime(file))
                                               .ToList();
                        // Lấy file mới nhất nếu có
                        if (logFiles.Any())
                        {
                            string latestFile = logFiles.First(); // Lấy file mới nhất
                            var directoryName = Directory.CreateDirectory(parentDir).Name;
                            // Nếu có platform và file tồn tại, tạo đường dẫn URL
                            if (System.IO.File.Exists(latestFile))
                            {
                                string fileName = Path.GetFileName(latestFile);
                                string fileUrl = $"{_appSetting.CurrentLink}{directoryName}/{folderName}/{fileName}";
                                outputFilePaths.Add(fileUrl);
                            }

                        }
                    }
                }
            }
            if (outputFilePaths.Count == 0)
            {
                response.Message = "Không có file log tồn tại trong khoảng thời gian yêu cầu";
            }
            else
            {
                response.Data = outputFilePaths;
            }
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseAppDTO<string>
            {
                Message = ErrorCodeEnum.Error.GetDescription(),
                ErrorCode = ErrorCodeEnum.Error,
                Data = ex.Message
            });
        }
    }
}