using FluentFTP;
using FluentFTP.Exceptions;

namespace HandleLog.Commons.Utils;

public class FTPConnect
{
    public static string UploadFileToFtp(string filePath, string ftpServerUrl, int port, string username,
        string password, string remoteFileName, string folderDay, string folderMonth, string folderYear, string baseUrlFTP)
    {
        using (FtpClient client = new FtpClient(ftpServerUrl, username, password, port))
        {
            try
            {
                // Kiểm tra kích thước tệp tin
                long fileSize = new FileInfo(filePath).Length;
                if (fileSize == 0)
                {
                    return "Tệp tin không có dữ liệu hoặc có kích thước 0 KB. Không thực hiện tải lên FTP.";
                }

                client.Connect();
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    var folderDate = $"{baseUrlFTP}/{folderYear}/{folderMonth}/{folderDay}";
                    client.CreateDirectory(folderDate);
                    client.UploadFile(filePath, remoteFileName);
                }

                return $" Upload File {remoteFileName} Thành Công - ";
            }
            catch (FtpCommandException ex)
            {
                if (ex.InnerException != null)
                {
                    return $"Inner Exception: {ex.InnerException.Message}";
                }
                return $"FTP Command Exception: {ex.Message}";
            }
            catch (FtpException ex)
            {
                if (ex.InnerException != null)
                {
                    return $"Inner Exception: {ex.InnerException.Message}";
                }
                return $"FTP Exception: {ex.Message}";
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return $"Inner Exception: {ex.InnerException.Message}";
                }
                return $"Exception: {ex.Message}";
            }
            finally
            {
                if (client.IsConnected)
                    client.Disconnect();
            }
        }
    }
}