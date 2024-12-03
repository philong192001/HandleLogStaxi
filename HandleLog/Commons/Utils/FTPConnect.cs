using FluentFTP;
using FluentFTP.Exceptions;
using LoggerELK.Enums;
using LoggerELK.Helpers;
using LoggerELK.Models;
using System.Reflection;

namespace HandleLog.Commons.Utils;

public class FTPConnect
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath">File đã merge - có đuôi _KH</param>
    /// <param name="ftpServerUrl">IP Server FTP</param>
    /// <param name="port">PORT SERVER FTP</param>
    /// <param name="username">Tên đăng nhập FTP</param>
    /// <param name="password">Mật khẩu FTP</param>
    /// <param name="remoteFileName"> Đường dẫn file đẩy lên server FTP</param>
    /// <param name="folderDay">Tên thư mục ngày</param>
    /// <param name="folderMonth">Tên thư mục tháng</param>
    /// <param name="folderYear">Tên thư mục năm</param>
    /// <param name="baseUrlFTP">Đường dẫn mặc định server FTP</param>
    /// <returns></returns>
    public static string UploadFileToFtp(string filePath, string ftpServerUrl, int port, string username,
        string password, string remoteFileName, string folderDay, string folderMonth, string folderYear, string baseUrlFTP, ELKSettings eLKSettings)
    {
        using (FtpClient client = new FtpClient(ftpServerUrl, username, password, port))
        {
            try
            {
                // Kiểm tra kích thước tệp tin
                long fileSize = new FileInfo(filePath).Length;
                if (fileSize == 0)
                {
                    Logging("Tệp tin không có dữ liệu hoặc có kích thước 0 KB -> Không thực hiện tải lên FTP.", eLKSettings);
                    return "False";
                }

                client.Connect();
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    var folderDate = $"{baseUrlFTP}/{folderYear}/{folderMonth}/{folderDay}";
                    client.CreateDirectory(folderDate);
                    client.UploadFile(filePath, remoteFileName);
                }
                Logging($" Upload File {remoteFileName} Thành Công",eLKSettings);

                return "True";
            }
            catch (FtpCommandException ex)
            {
                if (ex.InnerException != null)
                {
                    Logging($"Inner Exception: {ex.InnerException.Message}", eLKSettings);
                    return "False";
                }
                Logging($"FTP Command Exception: {ex.Message}", eLKSettings);
                return "False";
            }
            catch (FtpException ex)
            {
                if (ex.InnerException != null)
                {
                    Logging($"Inner Exception: {ex.InnerException.Message}", eLKSettings);
                    return "False";
                }
                Logging($"FTP Exception: {ex.Message}",eLKSettings);
                return "False";
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Logging($"Inner Exception: {ex.InnerException.Message}", eLKSettings);
                    return "False";
                }
                Logging($"Exception: {ex.Message}", eLKSettings);
                return "False";
            }
            finally
            {
                if (client.IsConnected)
                    client.Disconnect();
            }
        }
    }

    private static void Logging(string message, ELKSettings eLKSettings)
    {
        ELKHelper.SendLog(message, LevelLogELK.INFO, "/api/Logging/SendLogCustomer", "RESPONSE", Assembly.GetExecutingAssembly().GetName().Name,eLKSettings);
    }
}