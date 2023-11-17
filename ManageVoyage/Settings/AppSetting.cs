using ManageVoyage.Models;

namespace ManageVoyage.Settings;

public class AppSetting
{
    public UserCaro CaroAccount { get; set; }
    public string ConnectionString { get; set; }

    public static AppSetting MapValue(IConfiguration configuration)
    {
        var setting = new AppSetting
        {
            CaroAccount = configuration.GetSection("CaroAccount").Get<UserCaro>(),
            ConnectionString = configuration[nameof(ConnectionString)]
        };
        return setting;
    }
}
