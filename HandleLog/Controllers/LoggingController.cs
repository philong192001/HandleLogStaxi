using HandleLog.Commons.Enums;
using HandleLog.Commons.Utils;
using HandleLog.Contexts;
using HandleLog.Models;
using Microsoft.AspNetCore.Mvc;
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
            var getConfigFTP = _dbContext.Companies.Where(x => x.CompanyId.Equals(logRequest.CompanyId)).Select(x => new { x.IPServerFTP, x.PortFTP, x.UserNameFTP, x.PasswordFTP, x.DirectoryFTP, x.DirectoryLog }).FirstOrDefault();
            string[] directories = Directory.GetDirectories(_appSetting.PathRoot, $"{getConfigFTP.DirectoryLog}*");
            string msgWarnFile = "";
            string msgWarnFTP = "";

            //tách ngày tháng năm để tạo folder
            string[] parts = logRequest.Date.Split('_');
            string folderDay = parts[0];
            string folderMonth = parts[1];
            var folderYear = parts[2];

            // Loop through each directory
            foreach (string directory in directories)
            {
                // Combine the directory path and subfolder name
                string folderPathChild = Path.Combine(directory, logRequest.Date);
                var nameFileOutPut = $"TotalLog_{logRequest.VehicalPlate}{_appSetting.NameFileLogMerge}";

                // Create the output file path based on the subfolder name 
                string outputFilePath = Path.Combine(folderPathChild, nameFileOutPut);

                // Merge files in the subfolder
                var msgExecuteFile = FileUtil.MergeFilesInFolder(folderPathChild, outputFilePath, logRequest.VehicalPlate, _appSetting.AttrLog);
                msgWarnFile += msgExecuteFile;
                string patternAdr = $"{_appSetting.PathRoot}{getConfigFTP.DirectoryLog}*_Android";
                string patterniOS = $"{_appSetting.PathRoot}{getConfigFTP.DirectoryLog}*_iOS";
                //check type folder and change name remote file send by FTP
                if (Regex.IsMatch(directory.ToLower(), Regex.Escape(patterniOS.ToLower()).Replace("\\*", ".*")))
                {
                    string remoteFileNameiOS = $"{getConfigFTP.DirectoryFTP}/{folderYear}/{folderMonth}/{folderDay}/IOS_{nameFileOutPut}";
                    var resAction = FTPConnect.UploadFileToFtp(outputFilePath, getConfigFTP.IPServerFTP, getConfigFTP.PortFTP, getConfigFTP.UserNameFTP, getConfigFTP.PasswordFTP, remoteFileNameiOS, folderDay, folderMonth, folderYear, getConfigFTP.DirectoryFTP);
                    msgWarnFTP += resAction;
                }
                else if (Regex.IsMatch(directory.ToLower(), Regex.Escape(patternAdr.ToLower()).Replace("\\*", ".*")))
                {
                    string remoteFileNameAdr = $"{getConfigFTP.DirectoryFTP}/{folderYear}/{folderMonth}/{folderDay}/Android_{nameFileOutPut}";
                    var resAction = FTPConnect.UploadFileToFtp(outputFilePath, getConfigFTP.IPServerFTP, getConfigFTP.PortFTP, getConfigFTP.UserNameFTP, getConfigFTP.PasswordFTP, remoteFileNameAdr, folderDay, folderMonth, folderYear, getConfigFTP.DirectoryFTP);
                    msgWarnFTP += resAction;
                }
            }
            return Ok( new ResponseAppDTO<string>
            {
                Data = msgWarnFile + msgWarnFTP
            });
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