using LoggerELK.Models;

namespace HandleLog.Models;

public class AppSetting
{
    public string? AttrLog { get; set; }
    public string? PathRoot { get; set; }
    public string? NameFileLogMerge { get; set; }
    public string? ConnectionString { get; set; }
    /// <summary>
    /// Link mặc định service Log chung
    /// </summary>
    public string? CurrentLink { get; set; }
    public ELKSettings? ELKSettings { get; set; }

    public static AppSetting MapValue(IConfiguration configuration)
    {
        var setting = new AppSetting
        {
            AttrLog = configuration[nameof(AttrLog)],
            PathRoot = configuration[nameof(PathRoot)],
            NameFileLogMerge = configuration[nameof(NameFileLogMerge)],
            ConnectionString = configuration[nameof(ConnectionString)],
            CurrentLink = configuration[nameof(CurrentLink)],
            ELKSettings = configuration.GetSection(nameof(ELKSettings)).Get<ELKSettings>()
        };
        return setting;
    }
}