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

                    // Đường dẫn file log đã merge
                    string nameFileOutPut = $"TotalLog_{logRequest.VehicalPlate}{_appSetting.NameFileLogMerge}";
                    string outputFilePath = Path.Combine(subDir, nameFileOutPut).Replace("\\", "/");

                    // Merge tất cả file log theo biển số xe trong thư mục con
                    FileUtil.MergeFilesInFolder(subDir, outputFilePath, logRequest.VehicalPlate, _appSetting.AttrLog);

                    // Xác định platform (_Android hoặc _iOS)
                    string platformSuffix = parentDir.ToLower().Contains("_ios") ? "_iOS" :
                                            parentDir.ToLower().Contains("_android") ? "_Android" : "";

                    if (!string.IsNullOrEmpty(platformSuffix) && System.IO.File.Exists(outputFilePath))
                    {
                        string fileUrl = $"{_appSetting.CurrentLink}{getConfigFTP.DirectoryLog}{platformSuffix}/{folderName}/{nameFileOutPut}";
                        outputFilePaths.Add(fileUrl);
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