namespace LoggerELK.Models;

/// <summary>
/// Setting config ELK
/// </summary>
public class ELKSettings
{
    /// <summary>
    /// URL ELK
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// Tên đăng nhập ELK
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// Mật khẩu ELK
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// path api
    /// </summary>
    public string Path { get; set; }
}
