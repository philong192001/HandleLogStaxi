namespace HandleLog.Models;

public class AppSetting
{
    public string? AttrLog { get; set; }
    public string? PathRoot { get; set; }
    public string? NameFileLogMerge { get; set; }
    public string? ConnectionString { get; set; }

    public static AppSetting MapValue(IConfiguration configuration)
    {
        var setting = new AppSetting
        {
            AttrLog = configuration[nameof(AttrLog)],
            PathRoot = configuration[nameof(PathRoot)],
            NameFileLogMerge = configuration[nameof(NameFileLogMerge)],
            ConnectionString = configuration[nameof(ConnectionString)]
        };
        return setting;
    }
}